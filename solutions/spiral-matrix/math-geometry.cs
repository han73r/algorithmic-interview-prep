public class Solution {
    public System.Collections.Generic.IList<int> SpiralOrder(int[][] matrix) {
        var result = new System.Collections.Generic.List<int>();
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) {
            return result;
        }

        int top = 0;
        int bottom = matrix.Length - 1;
        int left = 0;
        int right = matrix[0].Length - 1;

        while (top <= bottom && left <= right) {
            for (int col = left; col <= right; col++) {
                result.Add(matrix[top][col]);
            }
            top++;

            for (int row = top; row <= bottom; row++) {
                result.Add(matrix[row][right]);
            }
            right--;

            if (top <= bottom) {
                for (int col = right; col >= left; col--) {
                    result.Add(matrix[bottom][col]);
                }
                bottom--;
            }

            if (left <= right) {
                for (int row = bottom; row >= top; row--) {
                    result.Add(matrix[row][left]);
                }
                left++;
            }
        }

        return result;
    }
}
