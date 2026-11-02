public class Solution {
    public int SnakesAndLadders(int[][] board) {
        int n = board.Length;
        var queue = new System.Collections.Generic.Queue<int>();
        queue.Enqueue(1);
        var visited = new bool[n * n + 1];
        visited[1] = true;
        int steps = 0;

        while (queue.Count > 0) {
            int size = queue.Count;
            for (int i = 0; i < size; i++) {
                int curr = queue.Dequeue();
                if (curr == n * n) {
                    return steps;
                }

                for (int next = curr + 1; next <= System.Math.Min(curr + 6, n * n); next++) {
                    var rc = GetCoordinates(next, n);
                    int r = rc.Item1;
                    int c = rc.Item2;
                    int destination = board[r][c] != -1 ? board[r][c] : next;

                    if (!visited[destination]) {
                        visited[destination] = true;
                        queue.Enqueue(destination);
                    }
                }
            }
            steps++;
        }

        return -1;
    }

    private System.Tuple<int, int> GetCoordinates(int s, int n) {
        int r = (s - 1) / n;
        int c = (s - 1) % n;
        if (r % 2 == 1) {
            c = n - 1 - c;
        }
        r = n - 1 - r;
        return new System.Tuple<int, int>(r, c);
    }
}
