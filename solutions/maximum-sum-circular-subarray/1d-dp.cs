public class Solution {
    public int MaxSubarraySumCircular(int[] nums) {
        int totalSum = 0;
        int currentMax = 0;
        int globalMax = nums[0];
        int currentMin = 0;
        int globalMin = nums[0];

        foreach (int num in nums) {
            currentMax = System.Math.Max(num, currentMax + num);
            globalMax = System.Math.Max(globalMax, currentMax);

            currentMin = System.Math.Min(num, currentMin + num);
            globalMin = System.Math.Min(globalMin, currentMin);

            totalSum += num;
        }

        if (globalMax < 0) {
            return globalMax;
        }

        return System.Math.Max(globalMax, totalSum - globalMin);
    }
}
