public class Solution {
    public System.Collections.Generic.IList<System.Collections.Generic.IList<int>> LevelOrder(TreeNode root) {
        var result = new System.Collections.Generic.List<System.Collections.Generic.IList<int>>();
        if (root == null) {
            return result;
        }

        var queue = new System.Collections.Generic.Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0) {
            int levelSize = queue.Count;
            var currentLevel = new System.Collections.Generic.List<int>();

            for (int i = 0; i < levelSize; i++) {
                var current = queue.Dequeue();
                currentLevel.Add(current.val);

                if (current.left != null) {
                    queue.Enqueue(current.left);
                }

                if (current.right != null) {
                    queue.Enqueue(current.right);
                }
            }

            result.Add(currentLevel);
        }

        return result;
    }
}
