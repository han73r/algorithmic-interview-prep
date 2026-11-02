public class Solution {
    private int minDiff = int.MaxValue;
    private int? prevVal = null;

    public int GetMinimumDifference(TreeNode root) {
        Inorder(root);
        return minDiff;
    }

    private void Inorder(TreeNode node) {
        if (node == null) {
            return;
        }

        Inorder(node.left);

        if (prevVal.HasValue) {
            minDiff = System.Math.Min(minDiff, node.val - prevVal.Value);
        }

        prevVal = node.val;

        Inorder(node.right);
    }
}
