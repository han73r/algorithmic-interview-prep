public class Solution {
    public System.Collections.Generic.IList<System.Collections.Generic.IList<int>> ZigzagLevelOrder(TreeNode root) {
        var result = new System.Collections.Generic.List<System.Collections.Generic.IList<int>>();
        if (root == null) {
            return result;
        }

        var queue = new System.Collections.Generic.Queue<TreeNode>();
        queue.Enqueue(root);
        bool leftToRight = true;

        while (queue.Count > 0) {
            int levelSize = queue.Count;
            var currentLevel = new int[levelSize];

            for (int i = 0; i < levelSize; i++) {
                var current = queue.Dequeue();
                int index = leftToRight ? i : levelSize - 1 - i;
                currentLevel[index] = current.val;

                if (current.left != null) {
                    queue.Enqueue(current.left);
                }

                if (current.right != null) {
                    queue.Enqueue(current.right);
                }
            }

            result.Add(new System.Collections.Generic.List<int>(currentLevel));
            leftToRight = !leftToRight;
        }

        return result;
    }
}
