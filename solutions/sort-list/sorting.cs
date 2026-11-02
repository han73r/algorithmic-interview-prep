public class Solution {
    public ListNode SortList(ListNode head) {
        if (head == null || head.next == null) {
            return head;
        }

        var mid = GetMid(head);
        var left = head;
        var right = mid.next;
        mid.next = null;

        left = SortList(left);
        right = SortList(right);

        return Merge(left, right);
    }

    private ListNode GetMid(ListNode head) {
        var slow = head;
        var fast = head;
        ListNode prev = null;

        while (fast != null && fast.next != null) {
            prev = slow;
            slow = slow.next;
            fast = fast.next.next;
        }

        return prev;
    }

    private ListNode Merge(ListNode l1, ListNode l2) {
        var dummy = new ListNode(0);
        var current = dummy;

        while (l1 != null && l2 != null) {
            if (l1.val < l2.val) {
                current.next = l1;
                l1 = l1.next;
            } else {
                current.next = l2;
                l2 = l2.next;
            }
            current = current.next;
        }

        current.next = l1 ?? l2;
        return dummy.next;
    }
}
