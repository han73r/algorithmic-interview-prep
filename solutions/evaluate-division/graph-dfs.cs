public class Solution {
    public double[] CalcEquation(System.Collections.Generic.IList<System.Collections.Generic.IList<string>> equations, double[] values, System.Collections.Generic.IList<System.Collections.Generic.IList<string>> queries) {
        var graph = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>>();

        for (int i = 0; i < equations.Count; i++) {
            string u = equations[i][0];
            string v = equations[i][1];
            double weight = values[i];

            if (!graph.ContainsKey(u)) {
                graph[u] = new System.Collections.Generic.Dictionary<string, double>();
            }
            if (!graph.ContainsKey(v)) {
                graph[v] = new System.Collections.Generic.Dictionary<string, double>();
            }

            graph[u][v] = weight;
            graph[v][u] = 1.0 / weight;
        }

        var result = new double[queries.Count];
        for (int i = 0; i < queries.Count; i++) {
            string start = queries[i][0];
            string end = queries[i][1];

            if (!graph.ContainsKey(start) || !graph.ContainsKey(end)) {
                result[i] = -1.0;
            } else if (start == end) {
                result[i] = 1.0;
            } else {
                var visited = new System.Collections.Generic.HashSet<string>();
                result[i] = Dfs(graph, start, end, 1.0, visited);
            }
        }

        return result;
    }

    private double Dfs(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, double>> graph, string current, string target, double currentProduct, System.Collections.Generic.HashSet<string> visited) {
        visited.Add(current);

        if (current == target) {
            return currentProduct;
        }

        foreach (var neighbor in graph[current]) {
            if (!visited.Contains(neighbor.Key)) {
                double res = Dfs(graph, neighbor.Key, target, currentProduct * neighbor.Value, visited);
                if (res != -1.0) {
                    return res;
                }
            }
        }

        return -1.0;
    }
}
