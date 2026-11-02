public class Solution {
    public Node Construct(int[][] grid) {
        return BuildTree(grid, 0, 0, grid.Length);
    }

    private Node BuildTree(int[][] grid, int r, int c, int length) {
        if (IsUniform(grid, r, c, length)) {
            return new Node(grid[r][c] == 1, true);
        }

        int half = length / 2;
        var node = new Node(true, false);
        node.topLeft = BuildTree(grid, r, c, half);
        node.topRight = BuildTree(grid, r, c + half, half);
        node.bottomLeft = BuildTree(grid, r + half, c, half);
        node.bottomRight = BuildTree(grid, r + half, c + half, half);

        return node;
    }

    private bool IsUniform(int[][] grid, int r, int c, int length) {
        int val = grid[r][c];
        for (int i = r; i < r + length; i++) {
            for (int j = c; j < c + length; j++) {
                if (grid[i][j] != val) {
                    return false;
                }
            }
        }
        return true;
    }
}
