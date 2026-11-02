public class Solution {
    private int _maxPathSum;

    public int MaxPathSum(TreeNode root) {
        _maxPathSum = int.MinValue;
        CalculateMaxGain(root);
        return _maxPathSum;
    }

    private int CalculateMaxGain(TreeNode node) {
        if (node == null) {
            return 0;
        }

        int leftGain = System.Math.Max(CalculateMaxGain(node.left), 0);
        int rightGain = System.Math.Max(CalculateMaxGain(node.right), 0);

        int currentPathSum = node.val + leftGain + rightGain;
        _maxPathSum = System.Math.Max(_maxPathSum, currentPathSum);

        return node.val + System.Math.Max(leftGain, rightGain);
    }
}
