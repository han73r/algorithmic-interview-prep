public class Solution {
    public int RomanToInt(string s) {
        if (s == null) {
            throw new ArgumentNullException(nameof(s));
        }

        int result = 0;
        int n = s.Length;

        for (int i = 0; i < n; i++) {
            int current = GetValue(s[i]);
            
            if (i + 1 < n && current < GetValue(s[i + 1])) {
                result -= current;
            } else {
                result += current;
            }
        }

        return result;
    }

    private int GetValue(char c) {
        switch (c) {
            case 'I': return 1;
            case 'V': return 5;
            case 'X': return 10;
            case 'L': return 50;
            case 'C': return 100;
            case 'D': return 500;
            case 'M': return 1000;
            default: throw new ArgumentException($"Invalid Roman numeral character: {c}", nameof(c));
        }
    }
}