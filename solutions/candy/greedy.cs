public class Solution {
    public int Candy(int[] ratings) {
        if (ratings == null) {
            throw new ArgumentNullException(nameof(ratings));
        }

        int n = ratings.Length;
        if (n == 0) {
            return 0;
        }

        int[] candies = new int[n];
        Array.Fill(candies, 1);

        for (int i = 1; i < n; i++) {
            if (ratings[i] > ratings[i - 1]) {
                candies[i] = candies[i - 1] + 1;
            }
        }

        int totalCandies = candies[n - 1];
        for (int i = n - 2; i >= 0; i--) {
            if (ratings[i] > ratings[i + 1]) {
                candies[i] = Math.Max(candies[i], candies[i + 1] + 1);
            }
            totalCandies += candies[i];
        }

        return totalCandies;
    }
}