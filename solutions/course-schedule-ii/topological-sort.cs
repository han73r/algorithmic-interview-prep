public class Solution {
    public int[] FindOrder(int numCourses, int[][] prerequisites) {
        var graph = new System.Collections.Generic.List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) {
            graph[i] = new System.Collections.Generic.List<int>();
        }

        foreach (var p in prerequisites) {
            graph[p[1]].Add(p[0]);
        }

        int[] state = new int[numCourses];
        var result = new System.Collections.Generic.List<int>();

        for (int i = 0; i < numCourses; i++) {
            if (state[i] == 0 && !Dfs(graph, i, state, result)) {
                return new int[0];
            }
        }

        result.Reverse();
        return result.ToArray();
    }

    private bool Dfs(System.Collections.Generic.List<int>[] graph, int node, int[] state, System.Collections.Generic.List<int> result) {
        if (state[node] == 1) {
            return false;
        }
        if (state[node] == 2) {
            return true;
        }

        state[node] = 1;

        foreach (var neighbor in graph[node]) {
            if (!Dfs(graph, neighbor, state, result)) {
                return false;
            }
        }

        state[node] = 2;
        result.Add(node);
        return true;
    }
}
