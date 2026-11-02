#!/usr/bin/env python3
"""
Build an Anki deck (.apkg) from:
    problems/<id>.json
    solutions/<id>/<approach>.json  (+ <approach>.cs next to it)
    docs/link_to_the_project.txt    (repo URL, used to build solution links)

Only NEW or CHANGED problems are exported. What was exported is tracked in
build/added.json (array of {id, added_at, hash}).

Usage:
    pip install genanki
    python scripts/build_deck.py             # export new/changed problems
    python scripts/build_deck.py --dry-run   # show what would happen
    python scripts/build_deck.py --all       # ignore the log, export everything
"""
import argparse
import hashlib
import html
import json
import random
import re
import sys
from datetime import datetime
from pathlib import Path

import genanki

ROOT = Path(__file__).resolve().parent.parent
PROBLEMS_DIR = ROOT / "problems"
SOLUTIONS_DIR = ROOT / "solutions"
BUILD_DIR = ROOT / "build"
LOG_FILE = BUILD_DIR / "added.json"
LINK_FILE = ROOT / "docs" / "link_to_the_project.txt"

DEFAULT_BRANCH = "main"
DECK_NAME = "Algorithms::Fast recognition"
# NEVER change these two: Anki uses them to recognise the deck/note type on re-import.
DECK_ID = 1_730_000_001
MODEL_ID = 1_730_000_002

REQUIRED = [
    "id", "title", "difficulty", "leetcode_url", "statement_short", "constraints",
    "hint", "keywords", "options", "answer", "why_not", "complexity",
]

# --------------------------------------------------------------------------- #
# Anki note type
# --------------------------------------------------------------------------- #

CSS = """
.card { font-family: Arial, sans-serif; font-size: 18px; text-align: left;
        max-width: 720px; margin: 0 auto; line-height: 1.4; }
.meta { font-size: 13px; opacity: .6; margin-bottom: 8px; }
.constraints { margin-top: 10px; font-family: Consolas, monospace; font-size: 15px; opacity: .85; }
.q { margin: 14px 0 6px; font-weight: bold; }
.opt { margin: 4px 0; }
.answer { font-size: 22px; font-weight: bold; margin-bottom: 4px; }
.title { margin-bottom: 10px; }
.hint { margin-top: 10px; font-style: italic; opacity: .85; }
.why ul, .solutions ul { margin: 6px 0; padding-left: 20px; }
.solutions { margin-top: 12px; font-size: 16px; }
.note { opacity: .7; font-size: 14px; }
mark { background: #ffe066; color: #000; padding: 0 2px; border-radius: 3px; }
.nightMode mark { background: #7a6a00; color: #fff; }
"""

QFMT = """
<div class="meta">{{Difficulty}}</div>
<div class="statement">{{Statement}}</div>
<div class="constraints">{{Constraints}}</div>
<div class="q">Which pattern?</div>
<div class="options">{{Options}}</div>
"""

AFMT = """
<div class="answer">{{Answer}}</div>
<div class="meta">{{Complexity}}</div>
<div class="title"><a href="{{LeetcodeUrl}}">{{Title}}</a></div>
<hr id="answer">
<div class="statement">{{StatementHL}}</div>
<div class="constraints">{{ConstraintsHL}}</div>
<div class="hint">{{HintHL}}</div>
<div class="why">{{WhyNot}}</div>
{{#Solutions}}<div class="solutions">{{Solutions}}</div>{{/Solutions}}
"""

MODEL = genanki.Model(
    MODEL_ID,
    "Pattern Recognition",
    fields=[
        {"name": "Id"}, {"name": "Title"}, {"name": "Difficulty"}, {"name": "LeetcodeUrl"},
        {"name": "Statement"}, {"name": "Constraints"}, {"name": "Options"},
        {"name": "Answer"}, {"name": "Complexity"},
        {"name": "StatementHL"}, {"name": "ConstraintsHL"}, {"name": "HintHL"},
        {"name": "WhyNot"}, {"name": "Solutions"},
    ],
    templates=[{"name": "Recognition", "qfmt": QFMT, "afmt": AFMT}],
    css=CSS,
)


class ProblemNote(genanki.Note):
    @property
    def guid(self):
        # Stable GUID from problem id: re-importing updates the note, never duplicates it.
        return genanki.guid_for(self.fields[0])


# --------------------------------------------------------------------------- #
# Helpers
# --------------------------------------------------------------------------- #

def slug(text: str) -> str:
    return re.sub(r"[^a-z0-9]+", "-", text.lower()).strip("-")


def highlight(text: str, keywords: list[str]) -> str:
    """Escape HTML and wrap keywords in <mark> in a single pass (longest first)."""
    if not keywords:
        return html.escape(text)
    pattern = re.compile("|".join(re.escape(k) for k in sorted(keywords, key=len, reverse=True)))
    out, pos = [], 0
    for m in pattern.finditer(text):
        out.append(html.escape(text[pos:m.start()]))
        out.append(f"<mark>{html.escape(m.group())}</mark>")
        pos = m.end()
    out.append(html.escape(text[pos:]))
    return "".join(out)


def render_options(options: list[str], answer: str, pid: str) -> tuple[str, str]:
    """Shuffle options with a stable per-problem seed. Returns (options_html, answer_label)."""
    shuffled = options[:]
    random.Random(pid).shuffle(shuffled)
    letters = "ABCD"
    rows, label = [], ""
    for letter, opt in zip(letters, shuffled):
        rows.append(f'<div class="opt"><b>{letter})</b> {html.escape(opt)}</div>')
        if opt == answer:
            label = f"{letter}) {answer}"
    return "".join(rows), label


def render_why(text: str) -> str:
    """Split 'Right: ... Not X: ... Not Y: ...' into list items."""
    parts = re.split(r"\s+(?=Not [^:]{1,40}:)", text.strip())
    items = []
    for p in parts:
        head, sep, rest = p.partition(":")
        if sep:
            items.append(f"<li><b>{html.escape(head)}:</b>{html.escape(rest)}</li>")
        else:
            items.append(f"<li>{html.escape(p)}</li>")
    return "<ul>" + "".join(items) + "</ul>"


def read_base_url() -> str | None:
    if not LINK_FILE.is_file():
        return None
    url = LINK_FILE.read_text(encoding="utf-8").strip().rstrip("/")
    return url or None


def solution_url(base: str | None, branch: str, pid: str, file: str) -> str | None:
    if not base:
        return None
    return f"{base}/blob/{branch}/solutions/{pid}/{file}"


def folder_url(base: str | None, branch: str, pid: str) -> str | None:
    if not base:
        return None
    return f"{base}/tree/{branch}/solutions/{pid}"


def render_solutions(pid: str, approaches: list[dict], base: str | None, branch: str) -> str:
    if not approaches:
        return ""
    furl = folder_url(base, branch, pid)
    header = (f'<a href="{html.escape(furl)}">Solutions (C#)</a>' if furl else "Solutions (C#)")
    header = f"<b>{header}</b>"
    ordered = sorted(approaches, key=lambda a: (not a.get("optimal", False), a.get("file", "")))
    items = []
    for a in ordered:
        raw_name = a.get("specification") or Path(a["file"]).stem
        name = html.escape(raw_name.replace("-", " "))
        url = solution_url(base, branch, pid, a["file"])
        title = f'<a href="{html.escape(url)}">{name}</a>' if url else name
        star = " &#10004; optimal" if a.get("optimal") else ""
        meta = f'{html.escape(a.get("time") or "?")} time, {html.escape(a.get("space") or "?")} space'
        note = f'<div class="note">{html.escape(a["note"])}</div>' if a.get("note") else ""
        items.append(f"<li>{title}{star}<br>{meta}{note}</li>")
    return header + "<ul>" + "".join(items) + "</ul>"


# --------------------------------------------------------------------------- #
# Loading and validation
# --------------------------------------------------------------------------- #

def load_approaches(pid: str) -> tuple[list[dict], list[str], list[str]]:
    """Read solutions/<id>/*.json. Returns (approaches, errors, warnings)."""
    folder = SOLUTIONS_DIR / pid
    approaches, errors, warnings = [], [], []
    if not folder.is_dir():
        return approaches, errors, warnings

    for meta in sorted(folder.glob("*.json")):
        try:
            a = json.loads(meta.read_text(encoding="utf-8"))
        except json.JSONDecodeError as e:
            errors.append(f"{meta.name}: invalid JSON ({e})")
            continue
        if not a.get("file"):
            errors.append(f"{meta.name}: missing 'file'")
            continue
        if not (folder / a["file"]).is_file():
            errors.append(f"{meta.name}: file '{a['file']}' not found")
            continue
        approaches.append(a)

    listed = {a["file"] for a in approaches}
    for cs in sorted(folder.glob("*.cs")):
        if cs.name not in listed:
            warnings.append(f"{cs.name}: no matching .json metadata, not shown on card")
    if sum(1 for a in approaches if a.get("optimal")) > 1:
        errors.append("more than one approach has optimal=true")
    return approaches, errors, warnings


def validate_problem(data: dict, path: Path) -> tuple[list[str], list[str]]:
    errors, warnings = [], []
    for f in REQUIRED:
        if data.get(f) in (None, "", []):
            errors.append(f"missing field '{f}'")
    if errors:
        return errors, warnings

    if data["id"] != path.stem:
        errors.append(f"id '{data['id']}' != file name '{path.stem}'")

    opts = data["options"]
    if len(opts) != 4 or len(set(opts)) != 4:
        errors.append("options must be exactly 4 unique items")
    if data["answer"] not in opts:
        errors.append("answer is not in options")
    if data.get("pattern") and data["pattern"] != data["answer"]:
        errors.append(f"pattern '{data['pattern']}' != answer '{data['answer']}'")

    text = " ".join([data["statement_short"], data["constraints"], data["hint"]])
    kws = data["keywords"]
    for k in kws:
        if k not in text:
            errors.append(f"keyword not found in text (case-sensitive): '{k}'")
    for a in kws:
        for b in kws:
            if a != b and a in b:
                errors.append(f"keyword '{a}' is a substring of '{b}'")

    for name in re.findall(r"Not ([^:]{1,40}):", data["why_not"]):
        if name.strip() not in opts:
            warnings.append(f"why_not mentions '{name.strip()}' which is not in options")
    return errors, warnings


def fingerprint(problem_path: Path, pid: str) -> str:
    """Hash of problem JSON + solution metadata, so edits trigger a re-export."""
    h = hashlib.sha1(problem_path.read_bytes())
    folder = SOLUTIONS_DIR / pid
    if folder.is_dir():
        for p in sorted(folder.glob("*.json")):
            h.update(p.read_bytes())
        for p in sorted(folder.glob("*.cs")):
            h.update(p.name.encode())
    return h.hexdigest()


def build_note(data: dict, approaches: list[dict], base: str | None, branch: str) -> ProblemNote:
    pid = data["id"]
    kws = data["keywords"]
    options_html, answer_label = render_options(data["options"], data["answer"], pid)

    tags = [slug(t) for t in data.get("tags", [])]
    tags += [slug(data["answer"]), data["difficulty"].lower()]
    tags = sorted({t for t in tags if t})

    return ProblemNote(
        model=MODEL,
        fields=[
            pid,
            html.escape(data["title"]),
            html.escape(data["difficulty"]),
            html.escape(data["leetcode_url"]),
            html.escape(data["statement_short"]),
            html.escape(data["constraints"]),
            options_html,
            html.escape(answer_label),
            html.escape(data["complexity"]),
            highlight(data["statement_short"], kws),
            highlight(data["constraints"], kws),
            highlight(data["hint"], kws),
            render_why(data["why_not"]),
            render_solutions(pid, approaches, base, branch),
        ],
        tags=tags,
    )


# --------------------------------------------------------------------------- #
# Log
# --------------------------------------------------------------------------- #

def load_log() -> list[dict]:
    if not LOG_FILE.is_file():
        return []
    return json.loads(LOG_FILE.read_text(encoding="utf-8"))


def save_log(entries: list[dict]) -> None:
    BUILD_DIR.mkdir(exist_ok=True)
    entries = sorted(entries, key=lambda e: e["id"])
    LOG_FILE.write_text(json.dumps(entries, indent=2, ensure_ascii=False), encoding="utf-8")


# --------------------------------------------------------------------------- #
# Main
# --------------------------------------------------------------------------- #

def main() -> int:
    ap = argparse.ArgumentParser(description="Build Anki deck from problems/*.json")
    ap.add_argument("--all", action="store_true", help="ignore the log, export everything")
    ap.add_argument("--dry-run", action="store_true", help="show what would be done, write nothing")
    ap.add_argument("--branch", default=DEFAULT_BRANCH, help=f"git branch for links (default: {DEFAULT_BRANCH})")
    args = ap.parse_args()

    base = read_base_url()
    if not base:
        print(f"[warn] {LINK_FILE} not found or empty: solution links will be plain text")

    log = {e["id"]: e for e in load_log()}
    deck = genanki.Deck(DECK_ID, DECK_NAME)
    exported: list[dict] = []
    skipped = failed = 0

    files = sorted(PROBLEMS_DIR.glob("*.json"))
    if not files:
        print(f"No problems found in {PROBLEMS_DIR}")
        return 0

    for path in files:
        try:
            data = json.loads(path.read_text(encoding="utf-8"))
        except json.JSONDecodeError as e:
            print(f"[ERROR] {path.name}: invalid JSON ({e})")
            failed += 1
            continue

        if not isinstance(data, dict):
            print(f"[ERROR] {path.name}: expected one JSON object {{...}}, "
                  f"got {type(data).__name__}. Split arrays into one file per problem.")
            failed += 1
            continue

        pid = data.get("id", path.stem)
        errors, warnings = validate_problem(data, path)
        approaches, sol_errors, sol_warnings = load_approaches(pid)
        errors += sol_errors
        warnings += sol_warnings

        for w in warnings:
            print(f"[warn]  {pid}: {w}")
        if errors:
            for e in errors:
                print(f"[ERROR] {pid}: {e}")
            failed += 1
            continue

        h = fingerprint(path, pid)
        prev = log.get(pid)
        if prev and prev["hash"] == h and not args.all:
            skipped += 1
            continue

        status = "REBUILD" if args.all and prev else ("UPDATED" if prev else "NEW")
        print(f"[{status}] {pid}  ({len(approaches)} solution(s))")
        deck.add_note(build_note(data, approaches, base, args.branch))
        exported.append({"id": pid, "added_at": datetime.now().isoformat(timespec="seconds"), "hash": h})

    print(f"\nnew/changed: {len(exported)}, unchanged: {skipped}, errors: {failed}")

    if exported and not args.dry_run:
        BUILD_DIR.mkdir(exist_ok=True)
        out = BUILD_DIR / f"deck_{datetime.now():%Y%m%d_%H%M%S}.apkg"
        genanki.Package(deck).write_to_file(out)
        for e in exported:
            log[e["id"]] = e
        save_log(list(log.values()))
        print(f"Written: {out}")
        print(f"Log updated: {LOG_FILE}")
    elif not exported:
        print("Nothing to export.")

    return 1 if failed else 0


if __name__ == "__main__":
    sys.exit(main())