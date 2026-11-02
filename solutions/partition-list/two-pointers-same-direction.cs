public class Solution {
    public ListNode Partition(ListNode head, int x) {
        ListNode lessDummy = new ListNode(0);
        ListNode greaterDummy = new ListNode(0);
        ListNode lessPtr = lessDummy;
        ListNode greaterPtr = greaterDummy;

        ListNode current = head;
        while (current != null) {
            if (current.val < x) {
                lessPtr.next = current;
                lessPtr = lessPtr.next;
            } else {
                greaterPtr.next = current;
                greaterPtr = greaterPtr.next;
            }
            current = current.next;
        }

        greaterPtr.next = null;
        lessPtr.next = greaterDummy.next;

        return lessDummy.next;
    }
}
