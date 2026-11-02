public class Solution {
    public int MaxProfit(int[] prices) {
        if (prices == null || prices.Length == 0) {
            throw new ArgumentException("Prices array cannot be null or empty.", nameof(prices));
        }

        int minPrice = prices[0];
        int maxProfit = 0;

        for (int i = 1; i < prices.Length; i++) {
            if (prices[i] < minPrice) {
                minPrice = prices[i];
            } else {
                int profit = prices[i] - minPrice;
                if (profit > maxProfit) {
                    maxProfit = profit;
                }
            }
        }

        return maxProfit;
    }
}