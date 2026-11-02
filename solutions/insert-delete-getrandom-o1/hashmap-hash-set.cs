using System;
using System.Collections.Generic;

public class RandomizedSet {
    private readonly List<int> _nums;
    private readonly Dictionary<int, int> _dict;
    private readonly Random _rand;

    public RandomizedSet() {
        _nums = new List<int>();
        _dict = new Dictionary<int, int>();
        _rand = new Random();
    }
    
    public bool Insert(int val) {
        if (_dict.ContainsKey(val)) {
            return false;
        }

        _nums.Add(val);
        _dict.Add(val, _nums.Count - 1);
        return true;
    }
    
    public bool Remove(int val) {
        if (!_dict.ContainsKey(val)) {
            return false;
        }

        int index = _dict[val];
        int lastElement = _nums[_nums.Count - 1];

        _nums[index] = lastElement;
        _dict[lastElement] = index;

        _dict.Remove(val);
        _nums.RemoveAt(_nums.Count - 1);

        return true;
    }
    
    public int GetRandom() {
        int randomIndex = _rand.Next(_nums.Count);
        return _nums[randomIndex];
    }
}