public class Solution {
    public bool CanFinish(int numCourses, int[][] prerequisites) {
        var graph = new System.Collections.Generic.List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) {
            graph[i] = new System.Collections.Generic.List<int>();
        }

        foreach (var p in prerequisites) {
            graph[p[1]].Add(p[0]);
        }

        int[] state = new int[numCourses];
        for (int i = 0; i < numCourses; i++) {
            if (state[i] == 0 && !HasCycle(graph, i, state)) {
                return false;
            }
        }

        return true;
    }

    private bool HasCycle(System.Collections.Generic.List<int>[] graph, int node, int[] state) {
        if (state[node] == 1) {
            return false;
        }
        if (state[node] == 2) {
            return true;
        }

        state[node] = 1;

        foreach (var neighbor in graph[node]) {
            if (!HasCycle(graph, neighbor, state)) {
                return false;
            }
        }

        state[node] = 2;
        return true;
    }
}
