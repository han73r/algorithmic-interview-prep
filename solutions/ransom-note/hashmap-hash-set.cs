public class Solution {
    public bool CanConstruct(string ransomNote, string magazine) {
        if (ransomNote.Length > magazine.Length) {
            return false;
        }

        var charCounts = new int[26];

        foreach (char c in magazine) {
            charCounts[c - 'a']++;
        }

        foreach (char c in ransomNote) {
            charCounts[c - 'a']--;
            if (charCounts[c - 'a'] < 0) {
                return false;
            }
        }

        return true;
    }
}
