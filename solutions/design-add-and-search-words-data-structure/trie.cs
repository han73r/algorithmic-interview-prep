public class WordDictionary {
    private readonly TrieNode root;

    public WordDictionary() {
        root = new TrieNode();
    }

    public void AddWord(string word) {
        var current = root;
        foreach (char c in word) {
            int index = c - 'a';
            if (current.Children[index] == null) {
                current.Children[index] = new TrieNode();
            }
            current = current.Children[index];
        }
        current.IsEnd = true;
    }

    public bool Search(string word) {
        return SearchHelper(word, 0, root);
    }

    private bool SearchHelper(string word, int index, TrieNode node) {
        if (node == null) {
            return false;
        }

        if (index == word.Length) {
            return node.IsEnd;
        }

        char c = word[index];
        if (c == '.') {
            for (int i = 0; i < 26; i++) {
                if (SearchHelper(word, index + 1, node.Children[i])) {
                    return true;
                }
            }
            return false;
        } else {
            int childIndex = c - 'a';
            return SearchHelper(word, index + 1, node.Children[childIndex]);
        }
    }

    private class TrieNode {
        public TrieNode[] Children { get; } = new TrieNode[26];
        public bool IsEnd { get; set; }
    }
}
