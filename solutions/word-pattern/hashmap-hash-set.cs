public class Solution {
    public bool WordPattern(string pattern, string s) {
        string[] words = s.Split(' ');
        if (pattern.Length != words.Length) {
            return false;
        }

        var charToWord = new System.Collections.Generic.Dictionary<char, string>();
        var wordToChar = new System.Collections.Generic.Dictionary<string, char>();

        for (int i = 0; i < pattern.Length; i++) {
            char c = pattern[i];
            string word = words[i];

            if (charToWord.ContainsKey(c) && charToWord[c] != word) {
                return false;
            }

            if (wordToChar.ContainsKey(word) && wordToChar[word] != c) {
                return false;
            }

            charToWord[c] = word;
            wordToChar[word] = c;
        }

        return true;
    }
}
