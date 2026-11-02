public class MedianFinder {
    private readonly System.Collections.Generic.PriorityQueue<int, int> maxHeap;
    private readonly System.Collections.Generic.PriorityQueue<int, int> minHeap;

    public MedianFinder() {
        maxHeap = new System.Collections.Generic.PriorityQueue<int, int>();
        minHeap = new System.Collections.Generic.PriorityQueue<int, int>();
    }

    public void AddNum(int num) {
        maxHeap.Enqueue(num, -num);
        minHeap.Enqueue(maxHeap.Dequeue(), maxHeap.Count);

        if (minHeap.Count > maxHeap.Count) {
            maxHeap.Enqueue(minHeap.Dequeue(), -minHeap.Count);
        }
    }

    public double FindMedian() {
        if (maxHeap.Count > minHeap.Count) {
            return maxHeap.Peek();
        }
        return (maxHeap.Peek() + minHeap.Peek()) / 2.0;
    }
}
