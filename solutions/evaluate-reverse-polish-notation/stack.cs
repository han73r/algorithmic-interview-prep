public class Solution {
    public int EvalRPN(string[] tokens) {
        var stack = new System.Collections.Generic.Stack<int>();

        foreach (string token in tokens) {
            if (token == "+") {
                int b = stack.Pop();
                int a = stack.Pop();
                stack.Push(a + b);
            } else if (token == "-") {
                int b = stack.Pop();
                int a = stack.Pop();
                stack.Push(a - b);
            } else if (token == "*") {
                int b = stack.Pop();
                int a = stack.Pop();
                stack.Push(a * b);
            } else if (token == "/") {
                int b = stack.Pop();
                int a = stack.Pop();
                stack.Push(a / b);
            } else {
                stack.Push(int.Parse(token));
            }
        }

        return stack.Pop();
    }
}
