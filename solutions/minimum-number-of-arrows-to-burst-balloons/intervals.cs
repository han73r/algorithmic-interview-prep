public class Solution {
    public int FindMinArrowShots(int[][] points) {
        if (points == null || points.Length == 0) {
            return 0;
        }

        System.Array.Sort(points, (a, b) => a[1].CompareTo(b[1]));

        int arrows = 1;
        int endCoord = points[0][1];

        for (int i = 1; i < points.Length; i++) {
            if (points[i][0] > endCoord) {
                arrows++;
                endCoord = points[i][1];
            }
        }

        return arrows;
    }
}
