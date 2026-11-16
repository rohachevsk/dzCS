namespace Primer;

using System;
using System.IO;

public static class Task4_FileReverser
{
    public static void Run(string? inputPath = null)
    {
        Console.WriteLine("--- Завдання 4: Перевертання вмісту файлу ---");

        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.Write("Введіть шлях до файлу (Enter для демо-файлу 'sample.txt'): ");
            string? input = Console.ReadLine()?.Trim();
            inputPath = string.IsNullOrWhiteSpace(input) ? "sample.txt" : input;

            if (!File.Exists(inputPath))
            {
                File.WriteAllText(inputPath, "Hello World!\nЦе тестовий рядок 12345.\nC# File Stream\n");
                Console.WriteLine($"Створено демо-файл: {inputPath}");
            }
        }

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Помилка: файл {inputPath} не знайдено!");
            return;
        }

        string originalText = File.ReadAllText(inputPath);
        Console.WriteLine("\nПочатковий вміст файлу:");
        Console.WriteLine(originalText);

        char[] charArray = originalText.ToCharArray();
        Array.Reverse(charArray);
        string reversedText = new string(charArray);

        string dir = Path.GetDirectoryName(inputPath) ?? "";
        string fileName = Path.GetFileNameWithoutExtension(inputPath);
        string ext = Path.GetExtension(inputPath);
        string outputPath = string.IsNullOrEmpty(dir)
            ? $"{fileName}_reversed{ext}"
            : Path.Combine(dir, $"{fileName}_reversed{ext}");

        File.WriteAllText(outputPath, reversedText);

        Console.WriteLine($"Перевернутий текст успішно збережено у новий файл: {outputPath}");
        Console.WriteLine("\nВміст нового перевернутого файлу:");
        Console.WriteLine(reversedText);
    }
}

