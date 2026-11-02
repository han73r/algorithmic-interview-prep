public class Solution {
    public System.Collections.Generic.IList<int> RightSideView(TreeNode root) {
        var result = new System.Collections.Generic.List<int>();
        if (root == null) {
            return result;
        }

        var queue = new System.Collections.Generic.Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0) {
            int levelSize = queue.Count;
            for (int i = 0; i < levelSize; i++) {
                var current = queue.Dequeue();
                if (i == levelSize - 1) {
                    result.Add(current.val);
                }

                if (current.left != null) {
                    queue.Enqueue(current.left);
                }

                if (current.right != null) {
                    queue.Enqueue(current.right);
                }
            }
        }

        return result;
    }
}
