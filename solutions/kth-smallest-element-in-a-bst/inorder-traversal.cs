public class Solution {
    public int KthSmallest(TreeNode root, int k) {
        var stack = new System.Collections.Generic.Stack<TreeNode>();
        var current = root;
        int count = 0;

        while (current != null || stack.Count > 0) {
            while (current != null) {
                stack.Push(current);
                current = current.left;
            }

            current = stack.Pop();
            count++;

            if (count == k) {
                return current.val;
            }

            current = current.right;
        }

        return -1;
    }
}
