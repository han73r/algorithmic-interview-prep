public class Solution {
    public int Trap(int[] height) {
        if (height == null) {
            throw new ArgumentNullException(nameof(height));
        }

        int left = 0;
        int right = height.Length - 1;
        int leftMax = 0;
        int rightMax = 0;
        int totalCompute = 0;

        while (left <= right) {
            if (leftMax <= rightMax) {
                if (height[left] > leftMax) {
                    leftMax = height[left];
                } else {
                    totalCompute += leftMax - height[left];
                }
                left++;
            } else {
                if (height[right] > rightMax) {
                    rightMax = height[right];
                } else {
                    totalCompute += rightMax - height[right];
                }
                right--;
            }
        }

        return totalCompute;
    }
}