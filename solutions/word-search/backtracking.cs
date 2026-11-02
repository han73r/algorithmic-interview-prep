public class Solution {
    public bool Exist(char[][] board, string word) {
        int rows = board.Length;
        int cols = board[0].Length;

        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                if (Backtrack(board, word, r, c, 0)) {
                    return true;
                }
            }
        }

        return false;
    }

    private bool Backtrack(char[][] board, string word, int r, int c, int index) {
        if (index == word.Length) {
            return true;
        }

        if (r < 0 || c < 0 || r >= board.Length || c >= board[0].Length || board[r][c] != word[index]) {
            return false;
        }

        char temp = board[r][c];
        board[r][c] = '#';

        bool found = Backtrack(board, word, r + 1, c, index + 1) ||
                     Backtrack(board, word, r - 1, c, index + 1) ||
                     Backtrack(board, word, r, c + 1, index + 1) ||
                     Backtrack(board, word, r, c - 1, index + 1);

        board[r][c] = temp;
        return found;
    }
}
