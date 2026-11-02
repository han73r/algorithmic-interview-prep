public class Solution {
    public int MajorityElement(int[] nums) {
        if (nums == null || nums.Length == 0) {
            throw new ArgumentException("Array cannot be null or empty.", nameof(nums));
        }

        int candidate = 0;
        int count = 0;

        foreach (int num in nums) {
            if (count == 0) {
                candidate = num;
            }

            count += (num == candidate) ? 1 : -1;
        }

        return candidate;
    }
}