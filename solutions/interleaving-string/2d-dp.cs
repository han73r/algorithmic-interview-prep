public class Solution {
    public bool IsInterleave(string s1, string s2, string s3) {
        int m = s1.Length;
        int n = s2.Length;
        if (m + n != s3.Length) {
            return false;
        }

        bool[] dp = new bool[n + 1];
        for (int j = 0; j <= n; j++) {
            if (s2.Substring(0, j) == s3.Substring(0, j)) {
                dp[j] = true;
            }
        }

        for (int i = 1; i <= m; i++) {
            dp[0] = dp[0] && s1[i - 1] == s3[i - 1];
            for (int j = 1; j <= n; j++) {
                dp[j] = (dp[j] && s1[i - 1] == s3[i + j - 1]) || (dp[j - 1] && s2[j - 1] == s3[i + j - 1]);
            }
        }

        return dp[n];
    }
}
