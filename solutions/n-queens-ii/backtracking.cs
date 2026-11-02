public class Solution {
    public int TotalNQueens(int n) {
        return Backtrack(0, 0, 0, 0, n);
    }

    private int Backtrack(int row, int cols, int diagonals1, int diagonals2, int n) {
        if (row == n) {
            return 1;
        }

        int count = 0;
        int availablePositions = ((1 << n) - 1) & ~(cols | diagonals1 | diagonals2);

        while (availablePositions > 0) {
            int p = availablePositions & -availablePositions;
            availablePositions -= p;
            count += Backtrack(row + 1, cols | p, (diagonals1 | p) << 1, (diagonals2 | p) >> 1, n);
        }

        return count;
    }
}
