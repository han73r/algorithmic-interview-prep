public class Solution {
    public bool IsValidBST(TreeNode root) {
        return Validate(root, null, null);
    }

    private bool Validate(TreeNode node, long? minVal, long? maxVal) {
        if (node == null) {
            return true;
        }

        if ((minVal.HasValue && node.val <= minVal.Value) || (maxVal.HasValue && node.val >= maxVal.Value)) {
            return false;
        }

        return Validate(node.left, minVal, node.val) && Validate(node.right, node.val, maxVal);
    }
}
