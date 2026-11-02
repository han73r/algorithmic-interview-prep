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
