#!/usr/bin/env python3
"""
Разбивает input_solutions.cs + input_solutions.json
на отдельные файлы: solutions/<kebab-namespace>/<file>.cs и <file>.json
"""
import json
import re
import sys
import textwrap
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent   # корень проекта (папка выше scripts)

CS_INPUT = ROOT / "input" / "input_solutions.cs"
JSON_INPUT = ROOT / "input" / "input_solutions.json"
OUT_DIR = ROOT / "solutions"

NS_RE = re.compile(r"^[ \t]*namespace\s+([\w.@]+)\s*\{", re.MULTILINE)


def find_closing_brace(src: str, open_idx: int) -> int:
    """Индекс парной '}' для '{' в open_idx. Пропускает строки и комментарии."""
    depth = 0
    i, n = open_idx, len(src)
    while i < n:
        c = src[i]
        nxt = src[i + 1] if i + 1 < n else ""

        if c == "/" and nxt == "/":                      # // комментарий
            j = src.find("\n", i)
            i = n if j == -1 else j
            continue
        if c == "/" and nxt == "*":                      # /* комментарий */
            j = src.find("*/", i + 2)
            i = n if j == -1 else j + 2
            continue

        # строки: "...", @"...", $"...", $@"...", @$"..."
        if c in "@$" and (nxt == '"' or (nxt in "@$" and src[i + 2:i + 3] == '"')):
            verbatim = "@" in src[i:i + 2]
            i += 2 if nxt != '"' else 1
            i = _skip_string(src, i, verbatim)
            continue
        if c == '"':
            if src.startswith('"""', i):                 # raw string literal
                j = src.find('"""', i + 3)
                i = n if j == -1 else j + 3
            else:
                i = _skip_string(src, i, False)
            continue

        if c == "'":                                     # символьный литерал
            i += 1
            while i < n and src[i] != "'":
                i += 2 if src[i] == "\\" else 1
            i += 1
            continue

        if c == "{":
            depth += 1
        elif c == "}":
            depth -= 1
            if depth == 0:
                return i
        i += 1
    raise ValueError("Не найдена закрывающая скобка namespace")


def _skip_string(src: str, i: int, verbatim: bool) -> int:
    """i указывает на открывающую кавычку. Возвращает индекс после закрывающей."""
    n = len(src)
    i += 1
    while i < n:
        c = src[i]
        if verbatim:
            if c == '"':
                if src[i + 1:i + 2] == '"':
                    i += 2
                    continue
                return i + 1
        else:
            if c == "\\":
                i += 2
                continue
            if c == '"':
                return i + 1
        i += 1
    return n


def parse_namespaces(src: str):
    """Возвращает (header, [(namespace_name, body), ...])"""
    result = []
    pos = 0
    header = None
    while True:
        m = NS_RE.search(src, pos)
        if not m:
            break
        if header is None:
            header = src[:m.start()].strip()
        open_idx = m.end() - 1
        close_idx = find_closing_brace(src, open_idx)
        body = src[open_idx + 1:close_idx]
        body = textwrap.dedent(body.strip("\n")).rstrip() + "\n"
        result.append((m.group(1), body))
        pos = close_idx + 1
    return header or "", result


def to_kebab(name: str) -> str:
    return name.lstrip("@_").replace("_", "-").replace(".", "-").lower()


def main():
    cs_src = CS_INPUT.read_text(encoding="utf-8-sig")
    meta = json.loads(JSON_INPUT.read_text(encoding="utf-8-sig"))

    header, namespaces = parse_namespaces(cs_src)

    if len(namespaces) != len(meta):
        sys.exit(
            f"Ошибка: namespace в .cs = {len(namespaces)}, "
            f"записей в .json = {len(meta)}. Ничего не записано."
        )

    seen = {}
    for idx, ((ns, body), info) in enumerate(zip(namespaces, meta), 1):
        folder = to_kebab(ns)
        if folder in seen:
            print(f"[!] Папка '{folder}' повторяется (#{seen[folder]} и #{idx}) "
                  f"- файлы будут перезаписаны")
        seen[folder] = idx

        file_name = Path(info["file"]).name          # защита от путей вида ../
        stem = Path(file_name).stem

        target = OUT_DIR / folder
        target.mkdir(parents=True, exist_ok=True)

        cs_text = (header + "\n\n" if header else "") + body
        (target / f"{stem}.cs").write_text(cs_text, encoding="utf-8", newline="\n")
        (target / f"{stem}.json").write_text(
            json.dumps(info, ensure_ascii=False, indent=2) + "\n",
            encoding="utf-8", newline="\n",
        )
        print(f"[{idx:>3}] {ns} -> {target}/{stem}.(cs|json)")

    print(f"\nГотово: {len(namespaces)} решений.")


if __name__ == "__main__":
    main()