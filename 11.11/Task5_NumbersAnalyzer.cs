namespace Primer;

using System;
using System.IO;

public static class Task5_NumbersAnalyzer
{
    public static void Run(string? numbersFilePath = null)
    {
        Console.WriteLine("--- Завдання 5: Аналіз 100 000 цілих чисел ---");

        if (string.IsNullOrWhiteSpace(numbersFilePath))
        {
            Console.Write("Введіть шлях до файлу (Enter для 'numbers.txt'): ");
            string? input = Console.ReadLine()?.Trim();
            numbersFilePath = string.IsNullOrWhiteSpace(input) ? "numbers.txt" : input;
        }

        if (!File.Exists(numbersFilePath))
        {
            Console.WriteLine($"Файл '{numbersFilePath}' не знайдено. Генеруємо 100 000 чисел...");
            GenerateRandomNumbers(numbersFilePath, 100000);
            Console.WriteLine("Генерацію завершено.");
        }

        string positiveFile = "positive_numbers.txt";
        string negativeFile = "negative_numbers.txt";
        string twoDigitsFile = "two_digits_numbers.txt";
        string fiveDigitsFile = "five_digits_numbers.txt";

        int positiveCount = 0;
        int negativeCount = 0;
        int twoDigitCount = 0;
        int fiveDigitCount = 0;
        int totalCount = 0;

        using (StreamReader reader = new StreamReader(numbersFilePath))
        using (StreamWriter posWriter = new StreamWriter(positiveFile))
        using (StreamWriter negWriter = new StreamWriter(negativeFile))
        using (StreamWriter twoWriter = new StreamWriter(twoDigitsFile))
        using (StreamWriter fiveWriter = new StreamWriter(fiveDigitsFile))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line, out int number))
                {
                    totalCount++;

                    if (number > 0)
                    {
                        positiveCount++;
                        posWriter.WriteLine(number);
                    }
                    else if (number < 0)
                    {
                        negativeCount++;
                        negWriter.WriteLine(number);
                    }

                    int abs = Math.Abs(number);

                    if (abs >= 10 && abs <= 99)
                    {
                        twoDigitCount++;
                        twoWriter.WriteLine(number);
                    }

                    if (abs >= 10000 && abs <= 99999)
                    {
                        fiveDigitCount++;
                        fiveWriter.WriteLine(number);
                    }
                }
            }
        }

        Console.WriteLine($"\nОброблено чисел: {totalCount}");
        Console.WriteLine("=== СТАТИСТИКА ===");
        Console.WriteLine($"1. Кількість додатних чисел:     {positiveCount,7}  -> збережено у '{positiveFile}'");
        Console.WriteLine($"2. Кількість від'ємних чисел:    {negativeCount,7}  -> збережено у '{negativeFile}'");
        Console.WriteLine($"3. Кількість двозначних чисел:   {twoDigitCount,7}  -> збережено у '{twoDigitsFile}'");
        Console.WriteLine($"4. Кількість п'ятизначних чисел: {fiveDigitCount,7}  -> збережено у '{fiveDigitsFile}'");
    }

    private static void GenerateRandomNumbers(string path, int count)
    {
        Random rnd = new Random();
        using StreamWriter writer = new StreamWriter(path);
        for (int i = 0; i < count; i++)
        {
            int num = rnd.Next(-100000, 100001);
            writer.WriteLine(num);
        }
    }
}
