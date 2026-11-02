public class Solution {
    public System.Collections.Generic.IList<string> SummaryRanges(int[] nums) {
        var result = new System.Collections.Generic.List<string>();
        if (nums == null || nums.Length == 0) {
            return result;
        }

        int i = 0;
        while (i < nums.Length) {
            int start = nums[i];
            while (i + 1 < nums.Length && nums[i + 1] == nums[i] + 1) {
                i++;
            }

            if (start == nums[i]) {
                result.Add(start.ToString());
            } else {
                result.Add($"{start}->{nums[i]}");
            }
            i++;
        }

        return result;
    }
}
