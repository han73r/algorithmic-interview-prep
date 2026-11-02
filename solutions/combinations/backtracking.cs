public class Solution {
    public System.Collections.Generic.IList<System.Collections.Generic.IList<int>> Combine(int n, int k) {
        var result = new System.Collections.Generic.List<System.Collections.Generic.IList<int>>();
        Backtrack(1, n, k, new System.Collections.Generic.List<int>(), result);
        return result;
    }

    private void Backtrack(int start, int n, int k, System.Collections.Generic.List<int> current, System.Collections.Generic.List<System.Collections.Generic.IList<int>> result) {
        if (current.Count == k) {
            result.Add(new System.Collections.Generic.List<int>(current));
            return;
        }

        int needed = k - current.Count;
        for (int i = start; i <= n - needed + 1; i++) {
            current.Add(i);
            Backtrack(i + 1, n, k, current, result);
            current.RemoveAt(current.Count - 1);
        }
    }
}
