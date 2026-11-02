public class Solution {
    public string Convert(string s, int numRows) {
        if (numRows == 1 || s.Length <= numRows) {
            return s;
        }

        var rows = new System.Text.StringBuilder[Math.Min(numRows, s.Length)];
        for (int i = 0; i < rows.Length; i++) {
            rows[i] = new System.Text.StringBuilder();
        }

        int currentRow = 0;
        bool goingDown = false;

        foreach (char c in s) {
            rows[currentRow].Append(c);
            if (currentRow == 0 || currentRow == numRows - 1) {
                goingDown = !goingDown;
            }
            currentRow += goingDown ? 1 : -1;
        }

        var result = new System.Text.StringBuilder();
        foreach (var row in rows) {
            result.Append(row);
        }

        return result.ToString();
    }
}
