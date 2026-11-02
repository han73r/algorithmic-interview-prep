public class Solution {
    public int LengthOfLIS(int[] nums) {
        var tails = new System.Collections.Generic.List<int>();

        foreach (int num in nums) {
            int left = 0;
            int right = tails.Count;

            while (left < right) {
                int mid = left + (right - left) / 2;
                if (tails[mid] < num) {
                    left = mid + 1;
                } else {
                    right = mid;
                }
            }

            if (left == tails.Count) {
                tails.Add(num);
            } else {
                tails[left] = num;
            }
        }

        return tails.Count;
    }
}
