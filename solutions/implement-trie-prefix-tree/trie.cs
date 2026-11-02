public class Trie {
    private readonly TrieNode root;

    public Trie() {
        root = new TrieNode();
    }

    public void Insert(string word) {
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
        var node = SearchPrefix(word);
        return node != null && node.IsEnd;
    }

    public bool StartsWith(string prefix) {
        return SearchPrefix(prefix) != null;
    }

    private TrieNode SearchPrefix(string prefix) {
        var current = root;
        foreach (char c in prefix) {
            int index = c - 'a';
            if (current.Children[index] == null) {
                return null;
            }
            current = current.Children[index];
        }
        return current;
    }

    private class TrieNode {
        public TrieNode[] Children { get; } = new TrieNode[26];
        public bool IsEnd { get; set; }
    }
}
