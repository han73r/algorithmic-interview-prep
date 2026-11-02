public class Solution {
    private static readonly string[] Mapping = new string[] {
        "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"
    };

    public System.Collections.Generic.IList<string> LetterCombinations(string digits) {
        var result = new System.Collections.Generic.List<string>();
        if (string.IsNullOrEmpty(digits)) {
            return result;
        }

        Backtrack(digits, 0, new System.Text.StringBuilder(), result);
        return result;
    }

    private void Backtrack(string digits, int index, System.Text.StringBuilder current, System.Collections.Generic.List<string> result) {
        if (index == digits.Length) {
            result.Add(current.ToString());
            return;
        }

        string letters = Mapping[digits[index] - '0'];
        foreach (char c in letters) {
            current.Append(c);
            Backtrack(digits, index + 1, current, result);
            current.Length--;
        }
    }
}
