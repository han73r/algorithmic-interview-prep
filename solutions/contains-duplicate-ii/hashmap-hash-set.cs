public class Solution {
    public bool ContainsNearbyDuplicate(int[] nums, int k) {
        var map = new System.Collections.Generic.Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++) {
            if (map.ContainsKey(nums[i]) && i - map[nums[i]] <= k) {
                return true;
            }
            map[nums[i]] = i;
        }

        return false;
    }
}
