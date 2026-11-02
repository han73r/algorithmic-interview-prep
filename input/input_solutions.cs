namespace max_points_on_a_line {
    public class Solution {
        public int MaxPoints(int[][] points) {
            int n = points.Length;
            if (n <= 2) {
                return n;
            }

            int maxPoints = 1;
            for (int i = 0; i < n; i++) {
                var slopeMap = new System.Collections.Generic.Dictionary<long, int>();
                int duplicates = 1;
                int localMax = 0;

                for (int j = i + 1; j < n; j++) {
                    int dx = points[j][0] - points[i][0];
                    int dy = points[j][1] - points[i][1];

                    if (dx == 0 && dy == 0) {
                        duplicates++;
                        continue;
                    }

                    int gcdVal = Gcd(dx, dy);
                    dx /= gcdVal;
                    dy /= gcdVal;

                    if (dx < 0 || (dx == 0 && dy < 0)) {
                        dx = -dx;
                        dy = -dy;
                    }

                    long slopeKey = ((long)dx << 32) | (dy & 0xFFFFFFFFL);
                    if (!slopeMap.ContainsKey(slopeKey)) {
                        slopeMap[slopeKey] = 0;
                    }
                    slopeMap[slopeKey]++;
                    localMax = System.Math.Max(localMax, slopeMap[slopeKey]);
                }

                maxPoints = System.Math.Max(maxPoints, localMax + duplicates);
            }

            return maxPoints;
        }

        private int Gcd(int a, int b) {
            while (b != 0) {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return System.Math.Abs(a);
        }
    }
}

namespace climbing_stairs {
    public class Solution {
        public int ClimbStairs(int n) {
            if (n <= 2) {
                return n;
            }

            int prev2 = 1;
            int prev1 = 2;
            int current = 0;

            for (int i = 3; i <= n; i++) {
                current = prev1 + prev2;
                prev2 = prev1;
                prev1 = current;
            }

            return prev1;
        }
    }
}

namespace house_robber {
    public class Solution {
        public int Rob(int[] nums) {
            if (nums == null || nums.Length == 0) {
                return 0;
            }

            int prev2 = 0;
            int prev1 = 0;

            foreach (int num in nums) {
                int current = System.Math.Max(prev1, prev2 + num);
                prev2 = prev1;
                prev1 = current;
            }

            return prev1;
        }
    }
}

namespace word_break {
    public class Solution {
        public bool WordBreak(string s, System.Collections.Generic.IList<string> wordDict) {
            var wordSet = new System.Collections.Generic.HashSet<string>(wordDict);
            int n = s.Length;
            bool[] dp = new bool[n + 1];
            dp[0] = true;

            for (int i = 1; i <= n; i++) {
                for (int j = 0; j < i; j++) {
                    if (dp[j] && wordSet.Contains(s.Substring(j, i - j))) {
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[n];
        }
    }
}

namespace coin_change {
    public class Solution {
        public int CoinChange(int[] coins, int amount) {
            int[] dp = new int[amount + 1];
            System.Array.Fill(dp, amount + 1);
            dp[0] = 0;

            for (int i = 1; i <= amount; i++) {
                foreach (int coin in coins) {
                    if (i - coin >= 0) {
                        dp[i] = System.Math.Min(dp[i], dp[i - coin] + 1);
                    }
                }
            }

            return dp[amount] > amount ? -1 : dp[amount];
        }
    }
}

namespace longest_increasing_subsequence {
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
}

namespace triangle {
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
}

namespace minimum_path_sum {
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
}

namespace unique_paths_ii {
    public class Solution {
        public int UniquePathsWithObstacles(int[][] obstacleGrid) {
            int m = obstacleGrid.Length;
            int n = obstacleGrid[0].Length;
            int[] dp = new int[n];
            dp[0] = obstacleGrid[0][0] == 1 ? 0 : 1;

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (obstacleGrid[i][j] == 1) {
                        dp[j] = 0;
                    } else if (j > 0) {
                        dp[j] += dp[j - 1];
                    }
                }
            }

            return dp[n - 1];
        }
    }
}

namespace longest_palindromic_substring {
    public class Solution {
        public string LongestPalindrome(string s) {
            if (string.IsNullOrEmpty(s)) {
                return string.Empty;
            }

            int start = 0;
            int maxLength = 0;

            for (int i = 0; i < s.Length; i++) {
                int len1 = ExpandAroundCenter(s, i, i);
                int len2 = ExpandAroundCenter(s, i, i + 1);
                int len = System.Math.Max(len1, len2);

                if (len > maxLength) {
                    maxLength = len;
                    start = i - (len - 1) / 2;
                }
            }

            return s.Substring(start, maxLength);
        }

        private int ExpandAroundCenter(string s, int left, int right) {
            while (left >= 0 && right < s.Length && s[left] == s[right]) {
                left--;
                right++;
            }
            return right - left - 1;
        }
    }
}

namespace interleaving_string {
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
}

namespace edit_distance {
    public class Solution {
        public int MinDistance(string word1, string word2) {
            int m = word1.Length;
            int n = word2.Length;
            int[] dp = new int[n + 1];

            for (int j = 0; j <= n; j++) {
                dp[j] = j;
            }

            for (int i = 1; i <= m; i++) {
                int prev = dp[0];
                dp[0] = i;
                for (int j = 1; j <= n; j++) {
                    int temp = dp[j];
                    if (word1[i - 1] == word2[j - 1]) {
                        dp[j] = prev;
                    } else {
                        dp[j] = 1 + System.Math.Min(prev, System.Math.Min(dp[j], dp[j - 1]));
                    }
                    prev = temp;
                }
            }

            return dp[n];
        }
    }
}

namespace best_time_to_buy_and_sell_stock_iv {
    public class Solution {
        public int MaxProfit(int k, int[] prices) {
            int n = prices.Length;
            if (n == 0 || k == 0) {
                return 0;
            }

            if (k >= n / 2) {
                int maxProfit = 0;
                for (int i = 1; i < n; i++) {
                    if (prices[i] > prices[i - 1]) {
                        maxProfit += prices[i] - prices[i - 1];
                    }
                }
                return maxProfit;
            }

            int[] buy = new int[k + 1];
            int[] sell = new int[k + 1];
            System.Array.Fill(buy, -prices[0]);

            for (int i = 1; i < n; i++) {
                for (int j = 1; j <= k; j++) {
                    buy[j] = System.Math.Max(buy[j], sell[j - 1] - prices[i]);
                    sell[j] = System.Math.Max(sell[j], buy[j] + prices[i]);
                }
            }

            return sell[k];
        }
    }
}

namespace maximal_square {
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
}