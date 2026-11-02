public class Solution {
    public void Rotate(int[] nums, int k) {
        if (nums == null) {
            throw new ArgumentNullException(nameof(nums));
        }

        int n = nums.Length;
        if (n <= 1) {
            return;
        }

        int effectiveK = k % n;
        if (effectiveK == 0) {
            return;
        }

        Array.Reverse(nums);
        Array.Reverse(nums, 0, effectiveK);
        Array.Reverse(nums, effectiveK, n - effectiveK);
    }
}