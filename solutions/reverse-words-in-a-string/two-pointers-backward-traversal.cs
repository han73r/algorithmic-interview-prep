public class Solution {
    public string ReverseWords(string s) {
        if (string.IsNullOrEmpty(s)) {
            return s;
        }

        var sb = new System.Text.StringBuilder();
        int i = s.Length - 1;

        while (i >= 0) {
            while (i >= 0 && s[i] == ' ') {
                i--;
            }

            if (i < 0) {
                break;
            }

            int j = i;
            while (j >= 0 && s[j] != ' ') {
                j--;
            }

            if (sb.Length > 0) {
                sb.Append(' ');
            }

            sb.Append(s, j + 1, i - j);
            i = j;
        }

        return sb.ToString();
    }
}
