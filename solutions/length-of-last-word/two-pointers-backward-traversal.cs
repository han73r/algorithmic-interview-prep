public class Solution {
    public int LengthOfLastWord(string s) {
        if (s == null) {
            throw new ArgumentNullException(nameof(s));
        }

        int length = 0;
        int i = s.Length - 1;

        while (i >= 0 && s[i] == ' ') {
            i--;
        }

        while (i >= 0 && s[i] != ' ') {
            length++;
            i--;
        }

        return length;
    }
}