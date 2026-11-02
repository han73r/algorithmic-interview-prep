public class Solution {
    public IList<string> FullJustify(string[] words, int maxWidth) {
        var result = new System.Collections.Generic.List<string>();
        int n = words.Length;
        int i = 0;

        while (i < n) {
            int j = i;
            int lineLength = 0;

            while (j < n && lineLength + words[j].Length + (j - i) <= maxWidth) {
                lineLength += words[j].Length;
                j++;
            }

            int diff = maxWidth - lineLength;
            int numberOfWords = j - i;
            var sb = new System.Text.StringBuilder();

            if (j == n || numberOfWords == 1) {
                for (int k = i; k < j; k++) {
                    sb.Append(words[k]);
                    if (k < j - 1) {
                        sb.Append(' ');
                    }
                }
                sb.Append(new string(' ', maxWidth - sb.Length));
            } else {
                int spacesBetweenWords = diff / (numberOfWords - 1);
                int extraSpaces = diff % (numberOfWords - 1);
                for (int k = i; k < j; k++) {
                    sb.Append(words[k]);
                    if (k < j - 1) {
                        int spacesToApply = spacesBetweenWords + (k - i < extraSpaces ? 1 : 0);
                        sb.Append(new string(' ', spacesToApply + 1));
                    }
                }
            }

            result.Add(sb.ToString());
            i = j;
        }

        return result;
    }
}
