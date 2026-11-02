public class LRUCache {
    private class Node {
        public int key;
        public int value;
        public Node prev;
        public Node next;

        public Node(int key, int value) {
            this.key = key;
            this.value = value;
        }
    }

    private readonly int _capacity;
    private readonly System.Collections.Generic.Dictionary<int, Node> _cache;
    private readonly Node _head;
    private readonly Node _tail;

    public LRUCache(int capacity) {
        _capacity = capacity;
        _cache = new System.Collections.Generic.Dictionary<int, Node>();
        _head = new Node(0, 0);
        _tail = new Node(0, 0);
        _head.next = _tail;
        _tail.prev = _head;
    }

    public int Get(int key) {
        if (!_cache.ContainsKey(key)) {
            return -1;
        }

        Node node = _cache[key];
        RemoveNode(node);
        AddToFront(node);
        return node.value;
    }

    public void Put(int key, int value) {
        if (_cache.ContainsKey(key)) {
            Node node = _cache[key];
            node.value = value;
            RemoveNode(node);
            AddToFront(node);
            return;
        }

        if (_cache.Count >= _capacity) {
            Node lruNode = _tail.prev;
            RemoveNode(lruNode);
            _cache.Remove(lruNode.key);
        }

        Node newNode = new Node(key, value);
        _cache[key] = newNode;
        AddToFront(newNode);
    }

    private void RemoveNode(Node node) {
        node.prev.next = node.next;
        node.next.prev = node.prev;
    }

    private void AddToFront(Node node) {
        node.prev = _head;
        node.next = _head.next;
        _head.next.prev = node;
        _head.next = node;
    }
}
