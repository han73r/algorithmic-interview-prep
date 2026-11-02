# leetcode-anki

Anki cards for **fast algorithm pattern recognition**: read a problem description and constraints, and instantly know which pattern solves it.

Problems and C# solutions live in this repo as plain files. A script builds the Anki deck from them.

## Card types

| # | Type | Trains | Status |
|---|------|--------|--------|
| 1 | Constraints | `n ≤ 10^5` → which complexity is allowed | planned |
| 2 | **Fast recognition** | description + constraints + 4 options → pattern | **in progress (main)** |
| 3 | Signal words | "sorted array" → Binary Search | planned |
| 4 | Realization | write the template of a pattern in C# | planned |

Fast recognition card:
- **Front:** short statement, constraints, 4 options (no interactivity).
- **Back:** correct pattern, highlighted signal words, why the distractors fail, complexity, link to solutions.

## Repo structure

```
/problems/
    problem-id.json           card data + list of approaches
/solutions/
   <problem-id>/              folder name = problem id (kebab-case)
      problem-id.cs           one approach per file, clean C# code 
      problem-id.json         folder name = problem id (kebab-case)
/patterns/                    pattern templates (for type 4 cards)
/scripts/                     JSON -> Anki deck builder
/build/                       generated .apkg (git-ignored)
```

## problem.json (short overview)

- Card fields: `leetcode_number`, `leetcode_url`, `title`, `difficulty`, `statement_short`, `constraints`, `hint`, `keywords`, `options`, `why_not`, `tags`.
- `approaches[]`: `file`, `pattern`, `time`, `space`, `optimal`, `note`.
- The card's answer and complexity are taken from the approach with `optimal: true`.

Full schema and rules: see [CONTRIBUTING.md](CONTRIBUTING.md).

## Build the deck

> TODO: filled in once the script is ready (planned: Python + `genanki`, output `build/*.apkg`).

The script will:
1. Collect all `problems/*/problem.json`.
2. Validate them (see CONTRIBUTING).
3. Shuffle options (stable seed per problem), highlight keywords, build the `.apkg`.
4. Build a pattern index ("all solutions using Two Pointers").

## Roadmap

- [ ] Finalize prompt + JSON schema
- [ ] Deck builder script with validation
- [ ] First batch: NeetCode 150, category by category
- [ ] Card types 1, 3, 4