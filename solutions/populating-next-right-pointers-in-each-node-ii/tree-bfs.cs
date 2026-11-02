public class Solution {
    public Node Connect(Node root) {
        if (root == null) {
            return null;
        }

        Node current = root;
        Node dummyHead = new Node(0);

        while (current != null) {
            Node tail = dummyHead;
            tail.next = null;

            while (current != null) {
                if (current.left != null) {
                    tail.next = current.left;
                    tail = tail.next;
                }
                if (current.right != null) {
                    tail.next = current.right;
                    tail = tail.next;
                }
                current = current.next;
            }

            current = dummyHead.next;
        }

        return root;
    }
}
