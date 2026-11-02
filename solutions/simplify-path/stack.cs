public class Solution {
    public string SimplifyPath(string path) {
        string[] components = path.Split('/');
        var stack = new System.Collections.Generic.Stack<string>();

        foreach (string component in components) {
            if (string.IsNullOrEmpty(component) || component == ".") {
                continue;
            }
            if (component == "..") {
                if (stack.Count > 0) {
                    stack.Pop();
                }
            } else {
                stack.Push(component);
            }
        }

        var result = new System.Text.StringBuilder();
        foreach (string dir in stack) {
            result.Insert(0, "/" + dir);
        }

        return result.Length == 0 ? "/" : result.ToString();
    }
}
