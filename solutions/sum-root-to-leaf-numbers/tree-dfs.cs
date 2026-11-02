public class Solution {
    public int SumNumbers(TreeNode root) {
        return CalculateSum(root, 0);
    }

    private int CalculateSum(TreeNode node, int currentSum) {
        if (node == null) {
            return 0;
        }

        currentSum = currentSum * 10 + node.val;

        if (node.left == null && node.right == null) {
            return currentSum;
        }

        return CalculateSum(node.left, currentSum) + CalculateSum(node.right, currentSum);
    }
}
