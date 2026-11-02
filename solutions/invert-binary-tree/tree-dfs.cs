public class Solution {
    public TreeNode InvertTree(TreeNode root) {
        if (root == null) {
            return null;
        }

        TreeNode leftTemp = InvertTree(root.left);
        TreeNode rightTemp = InvertTree(root.right);

        root.left = rightTemp;
        root.right = leftTemp;

        return root;
    }
}
