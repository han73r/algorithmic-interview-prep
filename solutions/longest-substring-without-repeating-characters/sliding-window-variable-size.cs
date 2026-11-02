public class Solution {
    public int LengthOfLongestSubstring(string s) {
        var charIndexMap = new System.Collections.Generic.Dictionary<char, int>();
        int maxLength = 0;
        int left = 0;

        for (int right = 0; right < s.Length; right++) {
            char c = s[right];
            if (charIndexMap.ContainsKey(c)) {
                left = System.Math.Max(left, charIndexMap[c] + 1);
            }
            charIndexMap[c] = right;
            maxLength = System.Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }
}
