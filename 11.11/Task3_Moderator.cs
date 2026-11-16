namespace Primer;

using System;
using System.IO;
using System.Text.RegularExpressions;

public static class Task3_Moderator
{
    public static void Run(string? textFilePath = null, string? badWordsFilePath = null)
    {
        Console.WriteLine("--- Завдання 3: Додаток «Модератор» ---");

        if (string.IsNullOrWhiteSpace(textFilePath) || string.IsNullOrWhiteSpace(badWordsFilePath))
        {
            Console.Write("Введіть шлях до файлу з текстом (Enter для створення демо-файлу 'text.txt'): ");
            string? input1 = Console.ReadLine()?.Trim();
            Console.Write("Введіть шлях до файлу зі словами для модерації (Enter для 'bad_words.txt'): ");
            string? input2 = Console.ReadLine()?.Trim();

            textFilePath = string.IsNullOrWhiteSpace(input1) ? "text.txt" : input1;
            badWordsFilePath = string.IsNullOrWhiteSpace(input2) ? "bad_words.txt" : input2;

            if (!File.Exists(textFilePath))
            {
                File.WriteAllText(textFilePath, "test best rest car\nman telephone\n");
                Console.WriteLine($"Створено демо-файл з текстом: {textFilePath}");
            }

            if (!File.Exists(badWordsFilePath))
            {
                File.WriteAllText(badWordsFilePath, "car telephone\n");
                Console.WriteLine($"Створено демо-файл зі стоп-словами: {badWordsFilePath}");
            }
        }

        if (!File.Exists(textFilePath))
        {
            Console.WriteLine($"Помилка: файл {textFilePath} не знайдено!");
            return;
        }

        if (!File.Exists(badWordsFilePath))
        {
            Console.WriteLine($"Помилка: файл {badWordsFilePath} не знайдено!");
            return;
        }

        string originalText = File.ReadAllText(textFilePath);
        Console.WriteLine("\nПочатковий текст у файлі:");
        Console.WriteLine(originalText);

        string[] badWords = File.ReadAllText(badWordsFilePath)
            .Split(new[] { ' ', '\r', '\n', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine("Слова для модерації: " + string.Join(", ", badWords));

        string moderatedText = originalText;
        foreach (string word in badWords)
        {
            string pattern = $@"\b{Regex.Escape(word)}\b";
            moderatedText = Regex.Replace(moderatedText, pattern, m => new string('*', m.Length), RegexOptions.IgnoreCase);
        }

        File.WriteAllText(textFilePath, moderatedText);

        Console.WriteLine("\nРезультат модерації (збережено у початковий файл):");
        Console.WriteLine(moderatedText);
    }
}

