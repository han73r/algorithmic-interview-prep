# Contributing

How to add or edit a problem.

## Workflow

### 1. Create problems

1. Use `docs/PROMT for problem generation.md` as the rules.
2. Use `docs/links.txt` as the source of problems.
3. Add the generated JSON array of problems to `input.json`.
4. Run:

   ```bash
   python scripts/json-parse-and-create.py
   ```

   The script creates a folder and a `.json` file for each `id` in `input.json`.

### 2. Create solutions

1. Use `docs/PROMT for solution generation.md` as the rules.
2. Send the problem to the LLM and receive a `.cs` file and a `.json` file.
3. Put both files into `solutions/<problem-id>/`.
4. Format the `.cs` file:

   ```bash
   dotnet format whitespace --folder .
   ```

5. Run the validator (once the script exists) and fix all errors before committing.

A problem without a `.cs` file is allowed: the card is built, the solution link is simply hidden.

## Problem JSON schema

```json
{
  "id": "remove-duplicates-from-sorted-array-ii",
  "leetcode_number": 80,
  "leetcode_url": "https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/",
  "title": "Remove Duplicates from Sorted Array II",
  "difficulty": "Medium",
  "pattern": "Two Pointers",
  "statement_short": "Given a sorted integer array, remove excess duplicates in-place such that each unique element appears at most twice while preserving relative order. Return the updated array length.",
  "constraints": "1 <= nums.length <= 3 * 10^4, -10^4 <= nums[i] <= 10^4",
  "hint": "Input size up to 3 * 10^4 requires O(n) time, and pre-sorted order permits fixed-offset duplicate checks.",
  "keywords": [
    "appears at most twice",
    "in-place",
    "-10^4 <= nums[i] <= 10^4"
  ],
  "options": [
    "1D DP",
    "Sliding Window",
    "HashMap / Hash Set",
    "Two Pointers"
  ],
  "answer": "Two Pointers",
  "why_not": "Right: Two Pointers compare the reader element against writer-2 to allow at most two copies in-place in O(n) time. Not 1D DP: Dynamic programming transitions are unnecessary for simple conditional array overwrite. Not Sliding Window: The problem asks for modifying sequence elements, not finding a max/min subsegment window. Not HashMap / Hash Set: Tracking element frequencies in a hash map uses O(n) auxiliary space.",
  "complexity": "O(n) time, O(1) space",
  "solution_link": "https://github.com/han73r/algorithmic-interview-prep/solutions/remove-duplicates-from-sorted-array-ii.cs",
  "tags": [
    "array",
    "two-pointers"
  ]
}
```

`id` is kebab-case and identical to the LeetCode slug and the folder name. There is no `approaches` field: each problem has one solution file with one optimal approach.

## Field rules

**id**
- kebab-case, same as the LeetCode slug and the folder name.

**statement_short**
- 1-3 sentences, your own words, never copy the LeetCode text.
- Keep every recognition signal (sorted/unsorted, contiguous, "exactly one solution", "k-th", "all possible").
- Do not name the pattern.

**constraints**
- Only numeric limits, copied accurately from LeetCode. Unsure → write `VERIFY` and check manually.

**hint**
- One line: what the limits imply. If n is tiny (≤ ~500), say limits do not restrict complexity and name the real signal (`in-place`, `O(1) space`, `sorted`).
- Never invent complexity requirements that are not in the statement.

**keywords**
- 2-6 phrases, each an **exact, case-sensitive substring** of `statement_short`, `constraints` or `hint` (the script highlights them).
- No keyword may be a substring of another keyword.

**pattern / answer**
- `pattern` and `answer` are the pattern of the optimal approach and must be identical.
- Both come from the closed list and are present in `options`.

**options**
- Exactly 4 patterns from the closed list, including the answer.
- Distractors must be plausible: a beginner could genuinely consider them. No filler.
- Order does not matter, the script shuffles (stable seed per problem).

**why_not**
- Max 2 short sentences for "Right:", 1 short sentence per distractor.
- Discuss only patterns that are in `options`.

**complexity**
- Time and space of the optimal solution, in the form `O(n) time, O(1) space`.

**solution_link**
- Link to the `.cs` file in the repository: `.../solutions/<problem-id>/<file>.cs`.

## Closed pattern list

Two Pointers, Sliding Window, Binary Search, Binary Search on Answer, HashMap / Hash Set, Prefix Sum, Stack, Monotonic Stack, Heap / Priority Queue, Linked List (Fast & Slow), Tree DFS, Tree BFS, Graph BFS, Graph DFS, Union-Find, Topological Sort, Trie, Backtracking, 1D DP, 2D DP, Greedy, Intervals, Bit Manipulation, Math / Geometry, Boyer-Moore Voting, Sorting.

Names must match exactly (no "two-pointer", no "Two pointers"). To add a pattern, change this list and the script's config in the same commit.

## Validation (planned checks)

- Folder name is kebab-case and equals `id`; the JSON is valid against the schema.
- `pattern` equals `answer`; both are in the closed list.
- `options` has 4 unique entries and contains `answer`.
- Every keyword is found in the text and none is a substring of another.
- `why_not` mentions no pattern outside `options`.
- If `solution_link` is set, the referenced `.cs` file exists.

## C# code conventions

- One solution per file, class named `Solution`.
- Clean code only: complexity and pattern metadata live in the JSON, not in comments.
- Short comments for the non-obvious step are welcome.
- Format with `dotnet format whitespace --folder .` before committing.

Example:

```csharp
using System;

public class Solution {
    public void Merge(int[] nums1, int m, int[] nums2, int n) {
        if (nums1 == null) {
            throw new ArgumentNullException(nameof(nums1));
        }
        if (nums2 == null) {
            throw new ArgumentNullException(nameof(nums2));
        }

        int index1 = m - 1;
        int index2 = n - 1;
        int mergeIndex = m + n - 1;

        while (index1 >= 0 && index2 >= 0) {
            if (nums1[index1] > nums2[index2]) {
                nums1[mergeIndex--] = nums1[index1--];
            } else {
                nums1[mergeIndex--] = nums2[index2--];
            }
        }

        while (index2 >= 0) {
            nums1[mergeIndex--] = nums2[index2--];
        }
    }
}
```

## Commits

One problem per commit: `add: merge-sorted-array`, `fix: remove-element constraints`.