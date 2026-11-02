public class Solution {
    public string AddBinary(string a, string b) {
        var sb = new System.Text.StringBuilder();
        int i = a.Length - 1;
        int j = b.Length - 1;
        int carry = 0;
        while (i >= 0 || j >= 0 || carry > 0) {
            int sum = carry;
            if (i >= 0) {
                sum += a[i] - '0';
                i--;
            }
            if (j >= 0) {
                sum += b[j] - '0';
                j--;
            }
            sb.Append(sum % 2);
            carry = sum / 2;
        }
        var charArray = sb.ToString().ToCharArray();
        System.Array.Reverse(charArray);
        return new string(charArray);
    }
}
