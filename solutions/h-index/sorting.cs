public class Solution {
    public int HIndex(int[] citations) {
        if (citations == null) {
            throw new ArgumentNullException(nameof(citations));
        }

        Array.Sort(citations);
        int n = citations.Length;

        for (int i = 0; i < n; i++) {
            if (citations[i] >= n - i) {
                return n - i;
            }
        }

        return 0;
    }
}