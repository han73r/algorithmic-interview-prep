public class Solution {
    public int MinimumTotal(System.Collections.Generic.IList<System.Collections.Generic.IList<int>> triangle) {
        int n = triangle.Count;
        int[] dp = new int[n];

        for (int j = 0; j < n; j++) {
            dp[j] = triangle[n - 1][j];
        }

        for (int i = n - 2; i >= 0; i--) {
            for (int j = 0; j <= i; j++) {
                dp[j] = triangle[i][j] + System.Math.Min(dp[j], dp[j + 1]);
            }
        }

        return dp[0];
    }
}
