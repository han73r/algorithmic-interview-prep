public class Solution {
    public bool IsAnagram(string s, string t) {
        if (s.Length != t.Length) {
            return false;
        }

        var counts = new int[26];

        foreach (char c in s) {
            counts[c - 'a']++;
        }

        foreach (char c in t) {
            counts[c - 'a']--;
            if (counts[c - 'a'] < 0) {
                return false;
            }
        }

        return true;
    }
}
