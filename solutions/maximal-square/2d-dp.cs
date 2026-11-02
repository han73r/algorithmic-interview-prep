public class Solution {
    public int MaximalSquare(char[][] matrix) {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) {
            return 0;
        }

        int m = matrix.Length;
        int n = matrix[0].Length;
        int[] dp = new int[n + 1];
        int maxSide = 0;
        int prev = 0;

        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                int temp = dp[j];
                if (matrix[i - 1][j - 1] == '1') {
                    dp[j] = System.Math.Min(MathMin(dp[j], dp[j - 1]), prev) + 1;
                    maxSide = System.Math.Max(maxSide, dp[j]);
                } else {
                    dp[j] = 0;
                }
                prev = temp;
            }
        }

        return maxSide * maxSide;
    }

    private int MathMin(int a, int b) {
        return System.Math.Min(a, b);
    }
}
