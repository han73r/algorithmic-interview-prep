public class Solution {
    public System.Collections.Generic.IList<System.Collections.Generic.IList<int>> CombinationSum(int[] candidates, int target) {
        var result = new System.Collections.Generic.List<System.Collections.Generic.IList<int>>();
        System.Array.Sort(candidates);
        Backtrack(candidates, target, 0, new System.Collections.Generic.List<int>(), result);
        return result;
    }

    private void Backtrack(int[] candidates, int target, int start, System.Collections.Generic.List<int> current, System.Collections.Generic.List<System.Collections.Generic.IList<int>> result) {
        if (target == 0) {
            result.Add(new System.Collections.Generic.List<int>(current));
            return;
        }

        for (int i = start; i < candidates.Length; i++) {
            if (candidates[i] > target) {
                break;
            }

            current.Add(candidates[i]);
            Backtrack(candidates, target - candidates[i], i, current, result);
            current.RemoveAt(current.Count - 1);
        }
    }
}
