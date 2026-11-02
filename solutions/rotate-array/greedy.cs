using System.Text;

public class Solution {
    public string IntToRoman(int num) {
        if (num <= 0 || num > 3999) {
            throw new ArgumentOutOfRangeException(nameof(num), "Integer must be between 1 and 3999.");
        }

        ReadOnlySpan<int> values = stackalloc int[] {
            1000,
            900,
            500,
            400,
            100,
            90,
            50,
            40,
            10,
            9,
            5,
            4,
            1
        };

        ReadOnlySpan<string> symbols = stackalloc string[] {
            "M",
            "CM",
            "D",
            "CD",
            "C",
            "XC",
            "L",
            "XL",
            "X",
            "IX",
            "V",
            "IV",
            "I"
        };

        var resultBuilder = new StringBuilder();

        for (int i = 0; i < values.Length; i++) {
            while (num >= values[i]) {
                resultBuilder.Append(symbols[i]);
                num -= values[i];
            }
        }

        return resultBuilder.ToString();
    }
}