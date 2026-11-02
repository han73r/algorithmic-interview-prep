public class Solution {
    public void GameOfLife(int[][] board) {
        int m = board.Length;
        int n = board[0].Length;
        int[] directions = { -1, 0, 1 };

        for (int r = 0; r < m; r++) {
            for (int c = 0; c < n; c++) {
                int liveNeighbors = 0;

                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 3; j++) {
                        if (directions[i] == 0 && directions[j] == 0) {
                            continue;
                        }

                        int nr = r + directions[i];
                        int nc = c + directions[j];

                        if (nr >= 0 && nr < m && nc >= 0 && nc < n && (board[nr][nc] & 1) == 1) {
                            liveNeighbors++;
                        }
                    }
                }

                if (board[r][c] == 1 && (liveNeighbors == 2 || liveNeighbors == 3)) {
                    board[r][c] |= 2;
                }

                if (board[r][c] == 0 && liveNeighbors == 3) {
                    board[r][c] |= 2;
                }
            }
        }

        for (int r = 0; r < m; r++) {
            for (int c = 0; c < n; c++) {
                board[r][c] >>= 1;
            }
        }
    }
}
