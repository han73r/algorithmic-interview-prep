public class Solution {
    public Node CopyRandomList(Node head) {
        if (head == null) {
            return null;
        }

        Node current = head;
        while (current != null) {
            Node nextNode = current.next;
            Node copy = new Node(current.val);
            current.next = copy;
            copy.next = nextNode;
            current = nextNode;
        }

        current = head;
        while (current != null) {
            if (current.random != null) {
                current.next.random = current.random.next;
            }
            current = current.next.next;
        }

        current = head;
        Node dummyHead = new Node(0);
        Node copyCurrent = dummyHead;

        while (current != null) {
            Node nextNode = current.next.next;
            Node copy = current.next;

            copyCurrent.next = copy;
            copyCurrent = copy;

            current.next = nextNode;
            current = nextNode;
        }

        return dummyHead.next;
    }
}
