public class Solution {
    public int MinPathSum(int[][] grid) {
        int m = grid.Length;
        int n = grid[0].Length;
        int[] dp = new int[n];

        for (int j = 0; j < n; j++) {
            dp[j] = (j == 0 ? 0 : dp[j - 1]) + grid[0][j];
        }

        for (int i = 1; i < m; i++) {
            dp[0] += grid[i][0];
            for (int j = 1; j < n; j++) {
                dp[j] = grid[i][j] + System.Math.Min(dp[j], dp[j - 1]);
            }
        }

        return dp[n - 1];
    }
}
