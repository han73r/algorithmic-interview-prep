public class MinStack {
    private readonly System.Collections.Generic.Stack<int> _stack;
    private readonly System.Collections.Generic.Stack<int> _minStack;

    public MinStack() {
        _stack = new System.Collections.Generic.Stack<int>();
        _minStack = new System.Collections.Generic.Stack<int>();
    }

    public void Push(int val) {
        _stack.Push(val);
        if (_minStack.Count == 0 || val <= _minStack.Peek()) {
            _minStack.Push(val);
        }
    }

    public void Pop() {
        int val = _stack.Pop();
        if (val == _minStack.Peek()) {
            _minStack.Pop();
        }
    }

    public int Top() {
        return _stack.Peek();
    }

    public int GetMin() {
        return _minStack.Peek();
    }
}
