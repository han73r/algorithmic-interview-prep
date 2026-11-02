public class Solution {
    private int _postorderIndex;
    private System.Collections.Generic.Dictionary<int, int> _inorderMap;

    public TreeNode BuildTree(int[] inorder, int[] postorder) {
        _postorderIndex = postorder.Length - 1;
        _inorderMap = new System.Collections.Generic.Dictionary<int, int>();
        for (int i = 0; i < inorder.Length; i++) {
            _inorderMap[inorder[i]] = i;
        }

        return ArrayToTree(postorder, 0, inorder.Length - 1);
    }

    private TreeNode ArrayToTree(int[] postorder, int left, int right) {
        if (left > right) {
            return null;
        }

        int rootValue = postorder[_postorderIndex--];
        TreeNode root = new TreeNode(rootValue);

        int index = _inorderMap[rootValue];

        root.right = ArrayToTree(postorder, index + 1, right);
        root.left = ArrayToTree(postorder, left, index - 1);

        return root;
    }
}
