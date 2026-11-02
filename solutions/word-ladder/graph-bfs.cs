public class Solution {
    public int LadderLength(string beginWord, string endWord, System.Collections.Generic.IList<string> wordList) {
        var wordSet = new System.Collections.Generic.HashSet<string>(wordList);
        if (!wordSet.Contains(endWord)) {
            return 0;
        }

        var queue = new System.Collections.Generic.Queue<string>();
        queue.Enqueue(beginWord);
        int level = 1;

        while (queue.Count > 0) {
            int size = queue.Count;
            for (int i = 0; i < size; i++) {
                string curr = queue.Dequeue();
                if (curr == endWord) {
                    return level;
                }

                char[] chars = curr.ToCharArray();
                for (int j = 0; j < chars.Length; j++) {
                    char originalChar = chars[j];
                    for (char c = 'a'; c <= 'z'; c++) {
                        if (c == originalChar) {
                            continue;
                        }
                        chars[j] = c;
                        string neighbor = new string(chars);

                        if (wordSet.Contains(neighbor)) {
                            wordSet.Remove(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                    chars[j] = originalChar;
                }
            }
            level++;
        }

        return 0;
    }
}
