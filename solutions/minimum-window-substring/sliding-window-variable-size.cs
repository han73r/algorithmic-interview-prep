public class Solution {
    public string MinWindow(string s, string t) {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length < t.Length) {
            return string.Empty;
        }

        var targetMap = new int[128];
        foreach (char c in t) {
            targetMap[c]++;
        }

        int required = 0;
        foreach (int count in targetMap) {
            if (count > 0) {
                required++;
            }
        }

        int formed = 0;
        var windowMap = new int[128];
        int left = 0;
        int minLen = int.MaxValue;
        int minLeft = 0;

        for (int right = 0; right < s.Length; right++) {
            char c = s[right];
            windowMap[c]++;

            if (targetMap[c] > 0 && windowMap[c] == targetMap[c]) {
                formed++;
            }

            while (left <= right && formed == required) {
                if (right - left + 1 < minLen) {
                    minLen = right - left + 1;
                    minLeft = left;
                }

                char leftChar = s[left];
                windowMap[leftChar]--;
                if (targetMap[leftChar] > 0 && windowMap[leftChar] < targetMap[leftChar]) {
                    formed--;
                }
                left++;
            }
        }

        return minLen == int.MaxValue ? string.Empty : s.Substring(minLeft, minLen);
    }
}
