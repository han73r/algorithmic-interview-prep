public class Solution {
    public int MinMutation(string startGene, string endGene, string[] bank) {
        var bankSet = new System.Collections.Generic.HashSet<string>(bank);
        if (!bankSet.Contains(endGene)) {
            return -1;
        }

        var queue = new System.Collections.Generic.Queue<string>();
        queue.Enqueue(startGene);
        var visited = new System.Collections.Generic.HashSet<string>();
        visited.Add(startGene);
        int mutations = 0;
        char[] chars = new char[] { 'A', 'C', 'G', 'T' };

        while (queue.Count > 0) {
            int size = queue.Count;
            for (int i = 0; i < size; i++) {
                string curr = queue.Dequeue();
                if (curr == endGene) {
                    return mutations;
                }

                char[] currArray = curr.ToCharArray();
                for (int j = 0; j < currArray.Length; j++) {
                    char oldChar = currArray[j];
                    foreach (char c in chars) {
                        if (c == oldChar) {
                            continue;
                        }
                        currArray[j] = c;
                        string neighbor = new string(currArray);

                        if (bankSet.Contains(neighbor) && !visited.Contains(neighbor)) {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                    currArray[j] = oldChar;
                }
            }
            mutations++;
        }

        return -1;
    }
}
