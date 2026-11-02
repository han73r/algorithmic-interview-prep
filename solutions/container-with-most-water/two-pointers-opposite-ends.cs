public class Solution {
    public int MaxArea(int[] height) {
        int maxArea = 0;
        int left = 0;
        int right = height.Length - 1;

        while (left < right) {
            int currentWidth = right - left;
            int currentHeight = System.Math.Min(height[left], height[right]);
            int currentArea = currentWidth * currentHeight;
            maxArea = System.Math.Max(maxArea, currentArea);

            if (height[left] < height[right]) {
                left++;
            } else {
                right--;
            }
        }

        return maxArea;
    }
}
