public class Solution {
    public int LongestConsecutive(int[] nums) {
        if (nums == null || nums.Length == 0) {
            return 0;
        }

        var numSet = new System.Collections.Generic.HashSet<int>();
        foreach (int num in nums) {
            numSet.Add(num);
        }

        int longestStreak = 0;

        foreach (int num in numSet) {
            if (!numSet.Contains(num - 1)) {
                int currentNum = num;
                int currentStreak = 1;

                while (numSet.Contains(currentNum + 1)) {
                    currentNum++;
                    currentStreak++;
                }

                longestStreak = System.Math.Max(longestStreak, currentStreak);
            }
        }

        return longestStreak;
    }
}
