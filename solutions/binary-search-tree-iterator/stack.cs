public class BSTIterator {
    private readonly System.Collections.Generic.Stack<TreeNode> _stack;

    public BSTIterator(TreeNode root) {
        _stack = new System.Collections.Generic.Stack<TreeNode>();
        PushLeftBranch(root);
    }

    public int Next() {
        TreeNode node = _stack.Pop();
        if (node.right != null) {
            PushLeftBranch(node.right);
        }
        return node.val;
    }

    public bool HasNext() {
        return _stack.Count > 0;
    }

    private void PushLeftBranch(TreeNode node) {
        while (node != null) {
            _stack.Push(node);
            node = node.left;
        }
    }
}
