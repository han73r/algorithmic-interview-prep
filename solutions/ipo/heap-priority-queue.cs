public class Solution {
    public int FindMaximizedCapital(int k, int w, int[] profits, int[] capital) {
        int n = profits.Length;
        var projects = new System.Collections.Generic.List<System.Tuple<int, int>>(n);
        for (int i = 0; i < n; i++) {
            projects.Add(new System.Collections.Generic.Tuple<int, int>(capital[i], profits[i]));
        }

        projects.Sort((a, b) => a.Item1.CompareTo(b.Item1));

        var maxHeap = new System.Collections.Generic.PriorityQueue<int, int>();
        int ptr = 0;

        for (int i = 0; i < k; i++) {
            while (ptr < n && projects[ptr].Item1 <= w) {
                maxHeap.Enqueue(projects[ptr].Item2, -projects[ptr].Item2);
                ptr++;
            }

            if (maxHeap.Count == 0) {
                break;
            }

            w += maxHeap.Dequeue();
        }

        return w;
    }
}
