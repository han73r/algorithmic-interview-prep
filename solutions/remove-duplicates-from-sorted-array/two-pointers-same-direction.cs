public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if (nums == null) {
            throw new ArgumentNullException(nameof(nums));
        }

        if (nums.Length == 0) {
            return 0;
        }

        int left = 0;

        for (int right = 1; right < nums.Length; right++) {
            if (nums[right] != nums[left]) {
                left++;
                nums[left] = nums[right];
            }
        }

        return left + 1;
    }
}