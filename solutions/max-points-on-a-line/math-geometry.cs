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
