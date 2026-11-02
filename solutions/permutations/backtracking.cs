public class Solution {
    public System.Collections.Generic.IList<System.Collections.Generic.IList<int>> Permute(int[] nums) {
        var result = new System.Collections.Generic.List<System.Collections.Generic.IList<int>>();
        Backtrack(nums, 0, result);
        return result;
    }

    private void Backtrack(int[] nums, int start, System.Collections.Generic.List<System.Collections.Generic.IList<int>> result) {
        if (start == nums.Length) {
            result.Add(new System.Collections.Generic.List<int>(nums));
            return;
        }

        for (int i = start; i < nums.Length; i++) {
            Swap(ref nums[start], ref nums[i]);
            Backtrack(nums, start + 1, result);
            Swap(ref nums[start], ref nums[i]);
        }
    }

    private void Swap(ref int a, ref int b) {
        int temp = a;
        a = b;
        b = temp;
    }
}
