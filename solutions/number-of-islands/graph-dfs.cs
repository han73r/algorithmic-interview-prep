public class Solution {
    public int NumIslands(char[][] grid) {
        if (grid == null || grid.Length == 0) {
            return 0;
        }

        int rows = grid.Length;
        int cols = grid[0].Length;
        int count = 0;

        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                if (grid[r][c] == '1') {
                    count++;
                    Dfs(grid, r, c, rows, cols);
                }
            }
        }

        return count;
    }

    private void Dfs(char[][] grid, int r, int c, int rows, int cols) {
        if (r < 0 || c < 0 || r >= rows || c >= cols || grid[r][c] != '1') {
            return;
        }

        grid[r][c] = '0';
        Dfs(grid, r - 1, c, rows, cols);
        Dfs(grid, r + 1, c, rows, cols);
        Dfs(grid, r, c - 1, rows, cols);
        Dfs(grid, r, c + 1, rows, cols);
    }
}
