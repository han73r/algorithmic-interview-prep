public class Solution {
    public System.Collections.Generic.IList<string> GenerateParenthesis(int n) {
        var result = new System.Collections.Generic.List<string>();
        Backtrack(new System.Text.StringBuilder(), 0, 0, n, result);
        return result;
    }

    private void Backtrack(System.Text.StringBuilder current, int open, int close, int n, System.Collections.Generic.List<string> result) {
        if (current.Length == 2 * n) {
            result.Add(current.ToString());
            return;
        }

        if (open < n) {
            current.Append('(');
            Backtrack(current, open + 1, close, n, result);
            current.Length--;
        }

        if (close < open) {
            current.Append(')');
            Backtrack(current, open, close + 1, n, result);
            current.Length--;
        }
    }
}
