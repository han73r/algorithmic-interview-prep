public class Solution {
    public void Solve(char[][] board) {
        if (board == null || board.Length == 0) {
            return;
        }

        int rows = board.Length;
        int cols = board[0].Length;

        for (int r = 0; r < rows; r++) {
            if (board[r][0] == 'O') {
                MarkBoundary(board, r, 0, rows, cols);
            }

            if (board[r][cols - 1] == 'O') {
                MarkBoundary(board, r, cols - 1, rows, cols);
            }
        }

        for (int c = 0; c < cols; c++) {
            if (board[0][c] == 'O') {
                MarkBoundary(board, 0, c, rows, cols);
            }

            if (board[rows - 1][c] == 'O') {
                MarkBoundary(board, rows - 1, c, rows, cols);
            }
        }

        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                if (board[r][c] == 'O') {
                    board[r][c] = 'X';
                } else if (board[r][c] == '#') {
                    board[r][c] = 'O';
                }
            }
        }
    }

    private void MarkBoundary(char[][] board, int r, int c, int rows, int cols) {
        if (r < 0 || c < 0 || r >= rows || c >= cols || board[r][c] != 'O') {
            return;
        }

        board[r][c] = '#';
        MarkBoundary(board, r - 1, c, rows, cols);
        MarkBoundary(board, r + 1, c, rows, cols);
        MarkBoundary(board, r, c - 1, rows, cols);
        MarkBoundary(board, r, c + 1, rows, cols);
    }
}
