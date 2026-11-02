public class Solution {
    public System.Collections.Generic.IList<System.Collections.Generic.IList<string>> GroupAnagrams(string[] strs) {
        var map = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>>();

        foreach (string str in strs) {
            var charCount = new int[26];
            foreach (char c in str) {
                charCount[c - 'a']++;
            }

            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 26; i++) {
                sb.Append('#');
                sb.Append(charCount[i]);
            }
            string key = sb.ToString();

            if (!map.ContainsKey(key)) {
                map[key] = new System.Collections.Generic.List<string>();
            }
            map[key].Add(str);
        }

        return new System.Collections.Generic.List<System.Collections.Generic.IList<string>>(map.Values);
    }
}
