public class Solution {
    public System.Collections.Generic.IList<int> FindSubstring(string s, string[] words) {
        var result = new System.Collections.Generic.List<int>();
        if (string.IsNullOrEmpty(s) || words.Length == 0) {
            return result;
        }

        int wordLength = words[0].Length;
        int wordCount = words.Length;
        int totalLength = wordLength * wordCount;

        if (s.Length < totalLength) {
            return result;
        }

        var wordMap = new System.Collections.Generic.Dictionary<string, int>();
        foreach (var word in words) {
            if (wordMap.ContainsKey(word)) {
                wordMap[word]++;
            } else {
                wordMap[word] = 1;
            }
        }

        for (int i = 0; i < wordLength; i++) {
            int left = i;
            int right = i;
            var currentMap = new System.Collections.Generic.Dictionary<string, int>();
            int count = 0;

            while (right + wordLength <= s.Length) {
                string subWord = s.Substring(right, wordLength);
                right += wordLength;

                if (wordMap.ContainsKey(subWord)) {
                    if (currentMap.ContainsKey(subWord)) {
                        currentMap[subWord]++;
                    } else {
                        currentMap[subWord] = 1;
                    }
                    count++;

                    while (currentMap[subWord] > wordMap[subWord]) {
                        string leftWord = s.Substring(left, wordLength);
                        currentMap[leftWord]--;
                        left += wordLength;
                        count--;
                    }

                    if (count == wordCount) {
                        result.Add(left);
                    }
                } else {
                    currentMap.Clear();
                    count = 0;
                    left = right;
                }
            }
        }

        return result;
    }
}
