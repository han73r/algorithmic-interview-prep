using System;
using System.Text;

public class Solution {
    public string LongestCommonPrefix(string[] strs) {
        if (strs == null || strs.Length == 0) {
            return string.Empty;
        }

        Array.Sort(strs);

        string first = strs[0];
        string last = strs[strs.Length - 1];
        int minLength = Math.Min(first.Length, last.Length);

        var prefixBuilder = new StringBuilder();
        for (int i = 0; i < minLength; i++) {
            if (first[i] != last[i]) {
                break;
            }
            prefixBuilder.Append(first[i]);
        }

        return prefixBuilder.ToString();
    }
}