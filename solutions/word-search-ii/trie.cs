public class Solution {
    public System.Collections.Generic.IList<string> FindWords(char[][] board, string[] words) {
        var root = BuildTrie(words);
        var result = new System.Collections.Generic.HashSet<string>();
        int rows = board.Length;
        int cols = board[0].Length;

        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                Dfs(board, r, c, root, result);
            }
        }

        return new System.Collections.Generic.List<string>(result);
    }

    private TrieNode BuildTrie(string[] words) {
        var root = new TrieNode();
        foreach (var word in words) {
            var current = root;
            foreach (char c in word) {
                int index = c - 'a';
                if (current.Children[index] == null) {
                    current.Children[index] = new TrieNode();
                }
                current = current.Children[index];
            }
            current.Word = word;
        }
        return root;
    }

    private void Dfs(char[][] board, int r, int c, TrieNode parent, System.Collections.Generic.HashSet<string> result) {
        char ch = board[r][c];
        int index = ch - 'a';
        if (ch == '#' || parent.Children[index] == null) {
            return;
        }

        var curr = parent.Children[index];
        if (curr.Word != null) {
            result.Add(curr.Word);
        }

        board[r][c] = '#';

        int[] rowOffsets = { -1, 1, 0, 0 };
        int[] colOffsets = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++) {
            int nextR = r + rowOffsets[i];
            int nextC = c + colOffsets[i];

            if (nextR >= 0 && nextR < board.Length && nextC >= 0 && nextC < board[0].Length && board[nextR][nextC] != '#') {
                Dfs(board, nextR, nextC, curr, result);
            }
        }

        board[r][c] = ch;
    }

    private class TrieNode {
        public TrieNode[] Children { get; } = new TrieNode[26];
        public string Word { get; set; }
    }
}
