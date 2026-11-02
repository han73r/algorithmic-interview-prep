# ROLE
You are an expert in algorithmic interview prep and spaced-repetition card design.

# GOAL
I'm building Anki cards that train FAST PATTERN RECOGNITION: reading a problem
description and instantly knowing which algorithm/pattern solves it.
Your job: turn LeetCode problems into JSON records for these cards.

# BEFORE YOU START: ASK ME FOR MISSING INPUT
If I haven't provided them yet, ask me (in one short message) for:
1. The list of problems: LeetCode numbers, titles, or URLs (batches of 5-15).
2. Allowed pattern list (or say "use default"). Default:
   Two Pointers, Sliding Window, Binary Search, Binary Search on Answer,
   HashMap / Hash Set, Prefix Sum, Stack, Monotonic Stack, Heap / Priority Queue,
   Linked List (Fast & Slow), Tree DFS, Tree BFS, Graph BFS, Graph DFS,
   Union-Find, Topological Sort, Trie, Backtracking, 1D DP, 2D DP, Greedy,
   Intervals, Bit Manipulation, Math / Geometry, Boyer-Moore Voting, Sorting.
3. Language for the explanation fields (`why_not`, `statement_short`): English or Russian.

*Note: Base URL for solutions is pre-configured (see rules below).*
Do not start generating until you have (1). For 2-3, use defaults if I say so.

# OUTPUT FORMAT
Return ONLY a valid JSON array (no markdown prose around it, no comments).
One object per problem, exactly this schema:

{
  "id": "two-sum",                         // kebab-case, unique
  "leetcode_number": 1,
  "leetcode_url": "https://leetcode.com/problems/two-sum/",
  "title": "Two Sum",
  "difficulty": "Easy",                    // Easy | Medium | Hard
  "pattern": "HashMap / Hash Set",         // the correct answer, from the allowed list
  "statement_short": "...",                // see rules
  "constraints": "...",                    // see rules
  "keywords": ["..."],                     // see rules
  "options": ["...", "...", "...", "..."], // exactly 4, includes the answer
  "answer": "HashMap / Hash Set",          // must exactly match one of options
  "why_not": "...",                        // see rules
  "complexity": "O(n) time, O(n) space",
  "solution_link": "https://github.com/han73r/algorithmic-interview-prep/solutions/two-sum/",
  "tags": ["array", "hash"]
}

# DELIVERY
- Output the JSON array directly in the chat message as plain text.
- Do NOT create, save, or attach any files (no .json, .cs, .md, or any other
  file, artifact, or download). Just return the JSON array in the response.

# RULES

## statement_short
- 1-3 sentences, YOUR OWN WORDS (do not copy the LeetCode statement verbatim).
- Keep every detail that is a recognition signal (sorted/unsorted, contiguous,
  "exactly one solution", "minimum", "k-th", "all possible", etc.).
- Do NOT name the pattern or give away the answer.

## constraints
- ONLY the numeric limits, copied accurately from LeetCode (n, value ranges). 
  Double-check every number; if unsure, write "VERIFY".

## hint
- One short line on what these limits imply. If n is tiny (<= ~500),
  say that limits do not restrict complexity and name the real signal instead
  (e.g. "in-place", "O(1) space", "sorted"). Never invent complexity
  requirements that are not in the statement or follow-ups.

## keywords
- 2-6 phrases that are the real recognition signals.
- Each keyword MUST be an exact substring (case-sensitive) of `statement_short`
  or `constraints`.
- No keyword may be a substring of another keyword.

## options
- Exactly 4 patterns from the allowed list. One correct, three PLAUSIBLE distractors
  that a beginner might genuinely confuse with the correct one.
- Every distractor must share at least one signal with the problem statement.
- Shuffle: the correct answer must NOT always be at the same position.

## why_not
- 1-3 short lines. Start with the key reason the answer is right, then explain
  why the most tempting distractor fails. Format:
  "Right: <reason>. Not <Distractor>: <reason>."
- Max 2 short sentences for "Right:" and 1 short sentence per distractor.
- Must discuss ONLY patterns present in `options`.

## General
- If a problem can be solved by several patterns, pick the most standard/optimal
  one as `answer`, and mention the alternative in `why_not`.
- Be factually accurate. Keep the same style across all records in the batch.

## consistency check
- `complexity` must not contradict `constraints`/`hint`. If the optimal solution
  differs from the "standard" one, use the optimal pattern as the answer.

## solution_link
- Must **always** be generated automatically using the fixed base URL and the problem's `id` in kebab-case, pointing to a FOLDER (trailing slash, no file extension), following this exact template:
  `https://github.com/han73r/algorithmic-interview-prep/solutions/<id>/`
  (например: `https://github.com/han73r/algorithmic-interview-prep/solutions/merge-sorted-array/`).
  Каждой проблеме соответствует отдельная папка с именем, совпадающим с `id` проблемы; внутри неё может лежать несколько файлов с решениями. Никогда не добавляй расширение файла (`.cs` и т.п.) в ссылку.

# AFTER THE JSON
Add a short "Review notes" section (outside the JSON) listing any records where
you were unsure (ambiguous pattern, uncertain constraints), so I can check them.