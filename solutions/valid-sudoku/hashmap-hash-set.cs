public class Solution {
    public bool IsValidSudoku(char[][] board) {
        var rows = new bool[9, 9];
        var cols = new bool[9, 9];
        var boxes = new bool[9, 9];

        for (int r = 0; r < 9; r++) {
            for (int c = 0; c < 9; c++) {
                char val = board[r][c];
                if (val == '.') {
                    continue;
                }

                int num = val - '1';
                int boxIndex = (r / 3) * 3 + (c / 3);

                if (rows[r, num] || cols[c, num] || boxes[boxIndex, num]) {
                    return false;
                }

                rows[r, num] = true;
                cols[c, num] = true;
                boxes[boxIndex, num] = true;
            }
        }

        return true;
    }
}
