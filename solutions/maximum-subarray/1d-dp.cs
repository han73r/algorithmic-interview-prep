public class Solution {
    public int MaxSubArray(int[] nums) {
        int currentMax = nums[0];
        int globalMax = nums[0];

        for (int i = 1; i < nums.Length; i++) {
            currentMax = System.Math.Max(nums[i], currentMax + nums[i]);
            globalMax = System.Math.Max(globalMax, currentMax);
        }

        return globalMax;
    }
}
