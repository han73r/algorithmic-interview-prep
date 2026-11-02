public class Solution {
    public bool IsIsomorphic(string s, string t) {
        if (s.Length != t.Length) {
            return false;
        }

        var sMap = new int[256];
        var tMap = new int[256];

        System.Array.Fill(sMap, -1);
        System.Array.Fill(tMap, -1);

        for (int i = 0; i < s.Length; i++) {
            char sc = s[i];
            char tc = t[i];

            if (sMap[sc] != tMap[tc]) {
                return false;
            }

            sMap[sc] = i;
            tMap[tc] = i;
        }

        return true;
    }
}
