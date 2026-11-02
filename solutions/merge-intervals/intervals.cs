public class Solution {
    public int[][] Merge(int[][] intervals) {
        if (intervals == null || intervals.Length <= 1) {
            return intervals;
        }

        System.Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
        var merged = new System.Collections.Generic.List<int[]>();
        int[] currentInterval = intervals[0];
        merged.Add(currentInterval);

        foreach (var interval in intervals) {
            int currentBegin = currentInterval[0];
            int currentEnd = currentInterval[1];
            int nextBegin = interval[0];
            int nextEnd = interval[1];

            if (nextBegin <= currentEnd) {
                currentInterval[1] = System.Math.Max(currentEnd, nextEnd);
            } else {
                currentInterval = interval;
                merged.Add(currentInterval);
            }
        }

        return merged.ToArray();
    }
}
