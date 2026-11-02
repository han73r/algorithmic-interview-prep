public class Solution {
    private readonly System.Collections.Generic.Dictionary<Node, Node> visited = new System.Collections.Generic.Dictionary<Node, Node>();

    public Node CloneGraph(Node node) {
        if (node == null) {
            return null;
        }

        if (visited.ContainsKey(node)) {
            return visited[node];
        }

        var cloneNode = new Node(node.val);
        visited[node] = cloneNode;

        foreach (var neighbor in node.neighbors) {
            cloneNode.neighbors.Add(CloneGraph(neighbor));
        }

        return cloneNode;
    }
}
