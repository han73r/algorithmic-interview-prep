public class Solution {
    public int CountNodes(TreeNode root) {
        if (root == null) {
            return 0;
        }

        int leftHeight = GetHeight(root.left);
        int rightHeight = GetHeight(root.right);

        if (leftHeight == rightHeight) {
            return (1 << leftHeight) + CountNodes(root.right);
        } else {
            return (1 << rightHeight) + CountNodes(root.left);
        }
    }

    private int GetHeight(TreeNode node) {
        int height = 0;
        while (node != null) {
            height++;
            node = node.left;
        }
        return height;
    }
}
