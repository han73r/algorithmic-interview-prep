public class Solution {
    public int RemoveElement(int[] nums, int val) {
        if (nums == null) {
            throw new ArgumentNullException(nameof(nums));
        }

        int left = 0;
        int right = nums.Length;

        while (left < right) {
            if (nums[left] == val) {
                nums[left] = nums[right - 1];
                right--;
            } else {
                left++;
            }
        }

        return right;
    }
}