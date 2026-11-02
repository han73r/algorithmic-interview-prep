## A 
Given an array of integers and a target, return indices of two numbers that add up to target. Exactly one solution. 2 ≤ n ≤ 10⁴, array is NOT sorted. Какой подход? 
- A) Two Pointers 
- B) HashMap
- C) Binary Search 
- D) Sliding Window
## B
✅ B) HashMap — O(n) 
Сигналы: "two numbers that add up" + "NOT sorted" → нельзя Two Pointers Почему не A: Two Pointers требует сортировки (иначе потеряем индексы) 
Решение: github.com/you/leetcode/TwoSum.cs


> 1. **Вариантов ответа лучше 4, и неправильные должны быть «правдоподобными»** (как в примере: Two Pointers для Two Sum выглядит логичным, но не подходит). Именно на таких отличиях и вырабатывается чутьё.
>2. **Один паттерн проверяй на разных задачах**, в том числе на «ловушках», где похожее описание, но другой паттерн.

> Без интерактивных тестов, просто с вариантами. Это проще и быстрее.