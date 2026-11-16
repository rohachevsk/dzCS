namespace Primer;

using System;
using System.Linq;

public static class Task2_StringQueries
{
    public static void Run()
    {
        Console.WriteLine("=== Завдання 2: LINQ для масиву рядків (міст) ===");

        string[] cities =
        {
            "London", "Paris", "Berlin", "Madrid", "Rome",
            "Amsterdam", "Brussels", "Vienna", "Prague", "Warsaw",
            "Budapest", "Athens", "Lisbon", "Dublin", "Copenhagen",
            "Stockholm", "Oslo", "Helsinki", "Zurich", "Geneva",
            "New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
            "Toronto", "Vancouver", "Montreal", "Calgary", "Ottawa",
            "Tokyo", "Osaka", "Kyoto", "Seoul", "Busan",
            "Beijing", "Shanghai", "Shenzhen", "Hong Kong", "Singapore",
            "Sydney", "Melbourne", "Brisbane", "Perth", "Adelaide"
        };

        Console.WriteLine("Початковий масив міст: " + string.Join(", ", cities));
        Console.WriteLine();

        Console.WriteLine("--- 1. Синтаксис запитів (Query Syntax) ---");

        var allCities = from c in cities
                        select c;
        Console.WriteLine("1. Весь масив міст:");
        Console.WriteLine(string.Join(", ", allCities));

        int minLen = 6;
        var longCities = from c in cities
                         where c.Length > minLen
                         select c;
        Console.WriteLine($"\n2. Міста з довжиною назви більше {minLen} символів:");
        Console.WriteLine(string.Join(", ", longCities));

        var startWithL = from c in cities
                         where c.StartsWith("L", StringComparison.OrdinalIgnoreCase)
                         select c;
        Console.WriteLine("\n3. Міста, що починаються на 'L':");
        Console.WriteLine(string.Join(", ", startWithL));

        var endWithA = from c in cities
                       where c.EndsWith("a", StringComparison.OrdinalIgnoreCase)
                       select c;
        Console.WriteLine("\n4. Міста, що закінчуються на 'a':");
        Console.WriteLine(string.Join(", ", endWithA));

        var sortedAlpha = from c in cities
                          orderby c ascending
                          select c;
        Console.WriteLine("\n5. Міста, відсортовані за алфавітом:");
        Console.WriteLine(string.Join(", ", sortedAlpha));

        var sortedByLength = from c in cities
                             orderby c.Length descending, c ascending
                             select $"{c} ({c.Length})";
        Console.WriteLine("\n6. Міста за спаданням довжини назви:");
        Console.WriteLine(string.Join(", ", sortedByLength));

        Console.WriteLine("\n--- 2. Методи розширення (Extension Methods) ---");

        var allCitiesExt = cities.Select(c => c);
        Console.WriteLine("1. Весь масив міст:");
        Console.WriteLine(string.Join(", ", allCitiesExt));

        var longCitiesExt = cities.Where(c => c.Length > minLen);
        Console.WriteLine($"\n2. Міста з довжиною назви більше {minLen} символів:");
        Console.WriteLine(string.Join(", ", longCitiesExt));

        var startWithLExt = cities.Where(c => c.StartsWith("L", StringComparison.OrdinalIgnoreCase));
        Console.WriteLine("\n3. Міста, що починаються на 'L':");
        Console.WriteLine(string.Join(", ", startWithLExt));

        var endWithAExt = cities.Where(c => c.EndsWith("a", StringComparison.OrdinalIgnoreCase));
        Console.WriteLine("\n4. Міста, що закінчуються на 'a':");
        Console.WriteLine(string.Join(", ", endWithAExt));

        var sortedAlphaExt = cities.OrderBy(c => c);
        Console.WriteLine("\n5. Міста, відсортовані за алфавітом:");
        Console.WriteLine(string.Join(", ", sortedAlphaExt));

        var sortedByLengthExt = cities.OrderByDescending(c => c.Length)
                                      .ThenBy(c => c)
                                      .Select(c => $"{c} ({c.Length})");
        Console.WriteLine("\n6. Міста за спаданням довжини назви:");
        Console.WriteLine(string.Join(", ", sortedByLengthExt));
    }
}
