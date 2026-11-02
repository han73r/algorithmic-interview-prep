public class Solution {
    public System.Collections.Generic.IList<double> AverageOfLevels(TreeNode root) {
        var result = new System.Collections.Generic.List<double>();
        if (root == null) {
            return result;
        }

        var queue = new System.Collections.Generic.Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0) {
            int levelSize = queue.Count;
            double sum = 0;

            for (int i = 0; i < levelSize; i++) {
                var current = queue.Dequeue();
                sum += current.val;

                if (current.left != null) {
                    queue.Enqueue(current.left);
                }

                if (current.right != null) {
                    queue.Enqueue(current.right);
                }
            }

            result.Add(sum / levelSize);
        }

        return result;
    }
}
