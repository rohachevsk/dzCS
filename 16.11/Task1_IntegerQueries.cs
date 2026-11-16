namespace Primer;

using System;
using System.Linq;

public static class Task1_IntegerQueries
{
    public static void Run()
    {
        Console.WriteLine("=== Завдання 1: LINQ для масиву цілих чисел ===");
        int[] numbers = { 14, 8, 21, 3, 16, 24, 7, 35, 40, 2, 49, 32, 5, 56, 64, 70, 80, 11, 4 };
        Console.WriteLine("Початковий масив: " + string.Join(", ", numbers));
        Console.WriteLine();

        Console.WriteLine("--- 1. Синтаксис запитів (Query Syntax) ---");

        var allNumbers = from n in numbers
                         select n;
        Console.WriteLine("1. Весь масив цілих:");
        Console.WriteLine(string.Join(", ", allNumbers));

        var evenNumbers = from n in numbers
                          where n % 2 == 0
                          select n;
        Console.WriteLine("\n2. Парні цілі:");
        Console.WriteLine(string.Join(", ", evenNumbers));

        var oddNumbers = from n in numbers
                         where n % 2 != 0
                         select n;
        Console.WriteLine("\n3. Непарні цілі:");
        Console.WriteLine(string.Join(", ", oddNumbers));

        int threshold = 25;
        var greaterThan = from n in numbers
                          where n > threshold
                          select n;
        Console.WriteLine($"\n4. Значення більше {threshold}:");
        Console.WriteLine(string.Join(", ", greaterThan));

        int min = 15;
        int max = 50;
        var inRange = from n in numbers
                      where n >= min && n <= max
                      select n;
        Console.WriteLine($"\n5. Числа в діапазоні від {min} до {max}:");
        Console.WriteLine(string.Join(", ", inRange));

        var multiplesOfSeven = from n in numbers
                               where n % 7 == 0
                               orderby n ascending
                               select n;
        Console.WriteLine("\n6. Числа, кратні 7 (за зростанням):");
        Console.WriteLine(string.Join(", ", multiplesOfSeven));

        var multiplesOfEight = from n in numbers
                               where n % 8 == 0
                               orderby n descending
                               select n;
        Console.WriteLine("\n7. Числа, кратні 8 (за спаданням):");
        Console.WriteLine(string.Join(", ", multiplesOfEight));

        Console.WriteLine("\n--- 2. Методи розширення (Extension Methods) ---");

        var allExt = numbers.Select(n => n);
        Console.WriteLine("1. Весь масив цілих:");
        Console.WriteLine(string.Join(", ", allExt));

        var evenExt = numbers.Where(n => n % 2 == 0);
        Console.WriteLine("\n2. Парні цілі:");
        Console.WriteLine(string.Join(", ", evenExt));

        var oddExt = numbers.Where(n => n % 2 != 0);
        Console.WriteLine("\n3. Непарні цілі:");
        Console.WriteLine(string.Join(", ", oddExt));

        var greaterExt = numbers.Where(n => n > threshold);
        Console.WriteLine($"\n4. Значення більше {threshold}:");
        Console.WriteLine(string.Join(", ", greaterExt));

        var inRangeExt = numbers.Where(n => n >= min && n <= max);
        Console.WriteLine($"\n5. Числа в діапазоні від {min} до {max}:");
        Console.WriteLine(string.Join(", ", inRangeExt));

        var multiplesOfSevenExt = numbers.Where(n => n % 7 == 0).OrderBy(n => n);
        Console.WriteLine("\n6. Числа, кратні 7 (за зростанням):");
        Console.WriteLine(string.Join(", ", multiplesOfSevenExt));

        var multiplesOfEightExt = numbers.Where(n => n % 8 == 0).OrderByDescending(n => n);
        Console.WriteLine("\n7. Числа, кратні 8 (за спаданням):");
        Console.WriteLine(string.Join(", ", multiplesOfEightExt));
    }
}
