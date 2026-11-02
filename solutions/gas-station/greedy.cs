public class Solution {
    public int CanCompleteCircuit(int[] gas, int[] cost) {
        if (gas == null || cost == null || gas.Length != cost.Length) {
            throw new ArgumentException("Gas and cost arrays must not be null and must have the same length.");
        }

        int totalTank = 0;
        int currentTank = 0;
        int startIndex = 0;

        for (int i = 0; i < gas.Length; i++) {
            int netGain = gas[i] - cost[i];
            totalTank += netGain;
            currentTank += netGain;

            if (currentTank < 0) {
                startIndex = i + 1;
                currentTank = 0;
            }
        }

        return totalTank >= 0 ? startIndex : -1;
    }
}