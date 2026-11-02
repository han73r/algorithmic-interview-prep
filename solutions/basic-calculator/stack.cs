public class Solution {
    public int Calculate(string s) {
        var stack = new System.Collections.Generic.Stack<int>();
        int result = 0;
        int number = 0;
        int sign = 1;

        for (int i = 0; i < s.Length; i++) {
            char c = s[i];

            if (char.IsDigit(c)) {
                number = number * 10 + (c - '0');
            } else if (c == '+') {
                result += sign * number;
                number = 0;
                sign = 1;
            } else if (c == '-') {
                result += sign * number;
                number = 0;
                sign = -1;
            } else if (c == '(') {
                stack.Push(result);
                stack.Push(sign);
                result = 0;
                sign = 1;
            } else if (c == ')') {
                result += sign * number;
                number = 0;
                result *= stack.Pop();
                result += stack.Pop();
            }
        }

        result += sign * number;
        return result;
    }
}
