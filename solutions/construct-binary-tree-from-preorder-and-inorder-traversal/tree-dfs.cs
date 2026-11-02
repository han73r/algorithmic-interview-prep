public class Solution {
    private int _preorderIndex;
    private System.Collections.Generic.Dictionary<int, int> _inorderMap;

    public TreeNode BuildTree(int[] preorder, int[] inorder) {
        _preorderIndex = 0;
        _inorderMap = new System.Collections.Generic.Dictionary<int, int>();
        for (int i = 0; i < inorder.Length; i++) {
            _inorderMap[inorder[i]] = i;
        }

        return ArrayToTree(preorder, 0, inorder.Length - 1);
    }

    private TreeNode ArrayToTree(int[] preorder, int left, int right) {
        if (left > right) {
            return null;
        }

        int rootValue = preorder[_preorderIndex++];
        TreeNode root = new TreeNode(rootValue);

        int index = _inorderMap[rootValue];

        root.left = ArrayToTree(preorder, left, index - 1);
        root.right = ArrayToTree(preorder, index + 1, right);

        return root;
    }
}
