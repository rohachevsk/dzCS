namespace Primer;

using System;
using System.Collections.Generic;

internal static class Program
{
    private static readonly PoemCollection collection = new();

    private static void Main(string[] args)
    {
        collection.SeedDefaultPoems();

        while (true)
        {
            Console.WriteLine("=== 13.11 Колекція віршів та генерація звітів ===");
            Console.WriteLine("1 — Показати всі вірші в колекції");
            Console.WriteLine("2 — Додати вірш (Завдання 1)");
            Console.WriteLine("3 — Видалити вірш (Завдання 1)");
            Console.WriteLine("4 — Змінити інформацію про вірш (Завдання 1)");
            Console.WriteLine("5 — Пошук вірша за характеристиками (Завдання 1)");
            Console.WriteLine("6 — Зберегти / завантажити колекцію (файл JSON, Завдання 1)");
            Console.WriteLine("7 — Звіти (Завдання 2 та 3)");
            Console.WriteLine("8 — Автоматична демонстрація всіх функцій");
            Console.WriteLine("0 — Вихід");
            Console.Write("Виберіть пункт: ");

            string? raw = Console.ReadLine();
            if (raw == null) return;
            string choice = raw.Trim();
            if (choice == "0") return;
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    PrintAllPoems();
                    break;
                case "2":
                    AddNewPoem();
                    break;
                case "3":
                    RemovePoem();
                    break;
                case "4":
                    EditPoem();
                    break;
                case "5":
                    SearchMenu();
                    break;
                case "6":
                    SaveLoadMenu();
                    break;
                case "7":
                    ReportsMenu();
                    break;
                case "8":
                    RunFullDemo();
                    break;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void PrintAllPoems()
    {
        Console.WriteLine($"Всього віршів у колекції: {collection.Count}");
        int i = 1;
        foreach (var p in collection.Poems)
        {
            Console.WriteLine($"{i++}. {p}");
        }
    }

    private static void AddNewPoem()
    {
        Console.Write("Введіть назву: ");
        string title = Console.ReadLine()?.Trim() ?? "";
        Console.Write("Введіть автора: ");
        string author = Console.ReadLine()?.Trim() ?? "";
        Console.Write("Введіть рік: ");
        int.TryParse(Console.ReadLine(), out int year);
        Console.Write("Введіть тему: ");
        string topic = Console.ReadLine()?.Trim() ?? "";
        Console.WriteLine("Введіть текст вірша (введіть рядок 'END' для завершення):");

        List<string> lines = new();
        while (true)
        {
            string? line = Console.ReadLine();
            if (line == null || line.Trim().Equals("END", StringComparison.OrdinalIgnoreCase))
                break;
            lines.Add(line);
        }

        string text = string.Join("\n", lines);
        collection.Add(new Poem(title, author, year, text, topic));
        Console.WriteLine("[OK] Вірш успішно додано!");
    }

    private static void RemovePoem()
    {
        Console.Write("Введіть назву вірша для видалення: ");
        string? title = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(title) && collection.Remove(title))
        {
            Console.WriteLine("[OK] Вірш видалено.");
        }
        else
        {
            Console.WriteLine("[!] Вірш з такою назвою не знайдено.");
        }
    }

    private static void EditPoem()
    {
        Console.Write("Введіть назву вірша, який хочете змінити: ");
        string? title = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(title)) return;

        Poem? existing = collection.FindByTitleExact(title);
        if (existing == null)
        {
            Console.WriteLine("[!] Вірш не знайдено.");
            return;
        }

        Console.Write($"Нова назва (Enter для залишення '{existing.Title}'): ");
        string? newTitle = Console.ReadLine()?.Trim();
        Console.Write($"Новий автор (Enter для залишення '{existing.Author}'): ");
        string? newAuthor = Console.ReadLine()?.Trim();
        Console.Write($"Новий рік (Enter для залишення {existing.Year}): ");
        string? yearStr = Console.ReadLine()?.Trim();
        int newYear = int.TryParse(yearStr, out int y) ? y : 0;
        Console.Write($"Нова тема (Enter для залишення '{existing.Topic}'): ");
        string? newTopic = Console.ReadLine()?.Trim();

        collection.Edit(title, newTitle ?? "", newAuthor ?? "", newYear, "", newTopic ?? "");
        Console.WriteLine("[OK] Дані вірша оновлено.");
    }

    private static void SearchMenu()
    {
        Console.WriteLine("--- Пошук віршів ---");
        Console.WriteLine("1 — За назвою");
        Console.WriteLine("2 — За автором");
        Console.WriteLine("3 — За роком");
        Console.WriteLine("4 — За темою");
        Console.WriteLine("5 — За словом у тексті");
        Console.Write("Ваш вибір: ");

        string? c = Console.ReadLine()?.Trim();
        List<Poem> res = new();

        switch (c)
        {
            case "1":
                Console.Write("Введіть назву: ");
                res = collection.SearchByTitle(Console.ReadLine()?.Trim() ?? "");
                break;
            case "2":
                Console.Write("Введіть автора: ");
                res = collection.SearchByAuthor(Console.ReadLine()?.Trim() ?? "");
                break;
            case "3":
                Console.Write("Введіть рік: ");
                int.TryParse(Console.ReadLine(), out int yr);
                res = collection.SearchByYear(yr);
                break;
            case "4":
                Console.Write("Введіть тему: ");
                res = collection.SearchByTopic(Console.ReadLine()?.Trim() ?? "");
                break;
            case "5":
                Console.Write("Введіть слово для пошуку в тексті: ");
                res = collection.SearchByWordInText(Console.ReadLine()?.Trim() ?? "");
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                return;
        }

        Console.WriteLine($"\nЗнайдено віршів: {res.Count}");
        foreach (var p in res)
        {
            Console.WriteLine(p);
        }
    }

    private static void SaveLoadMenu()
    {
        Console.WriteLine("1 — Зберегти колекцію у файл");
        Console.WriteLine("2 — Завантажити колекцію з файлу");
        Console.Write("Ваш вибір: ");
        string? c = Console.ReadLine()?.Trim();

        Console.Write("Введіть шлях до файлу (за замовчуванням 'poems.json'): ");
        string? path = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(path)) path = "poems.json";

        try
        {
            if (c == "1")
            {
                collection.SaveToFile(path);
                Console.WriteLine($"[OK] Колекцію успішно збережено у '{path}'.");
            }
            else if (c == "2")
            {
                collection.LoadFromFile(path);
                Console.WriteLine($"[OK] Колекцію успішно завантажено. Всього віршів: {collection.Count}.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

    private static void ReportsMenu()
    {
        Console.WriteLine("--- Генерація звітів (Завдання 2 та 3) ---");
        Console.WriteLine("1 — Звіт за назвою вірша");
        Console.WriteLine("2 — Звіт за ПІБ автора");
        Console.WriteLine("3 — Звіт за темою вірша");
        Console.WriteLine("4 — Звіт за словом у тексті вірша");
        Console.WriteLine("5 — Звіт за роком написання");
        Console.WriteLine("6 — Звіт за довжиною віршів");
        Console.Write("Виберіть звіт: ");

        string? c = Console.ReadLine()?.Trim();
        string reportText = "";

        switch (c)
        {
            case "1":
                Console.Write("Введіть назву: ");
                reportText = PoemReports.ReportByTitle(collection, Console.ReadLine()?.Trim() ?? "");
                break;
            case "2":
                Console.Write("Введіть автора: ");
                reportText = PoemReports.ReportByAuthor(collection, Console.ReadLine()?.Trim() ?? "");
                break;
            case "3":
                Console.Write("Введіть тему: ");
                reportText = PoemReports.ReportByTopic(collection, Console.ReadLine()?.Trim() ?? "");
                break;
            case "4":
                Console.Write("Введіть слово у тексті: ");
                reportText = PoemReports.ReportByWord(collection, Console.ReadLine()?.Trim() ?? "");
                break;
            case "5":
                Console.Write("Введіть рік: ");
                int.TryParse(Console.ReadLine(), out int yr);
                reportText = PoemReports.ReportByYear(collection, yr);
                break;
            case "6":
                reportText = PoemReports.ReportByLength(collection);
                break;
            default:
                Console.WriteLine("Невірний вибір.");
                return;
        }

        Console.Write("Зберегти звіт у файл? (введіть ім'я файлу, наприклад 'report.txt', або натисніть Enter для виводу лише на екран): ");
        string? saveFile = Console.ReadLine()?.Trim();

        PoemReports.ShowOrSave(reportText, string.IsNullOrWhiteSpace(saveFile) ? null : saveFile);
    }

    private static void RunFullDemo()
    {
        Console.WriteLine("=== ДЕМОНСТРАЦІЯ ЗАВДАНЬ 1, 2 ТА 3 ===");
        PrintAllPoems();

        Console.WriteLine("\n--- Завдання 2: Звіт за автором 'Тарас Шевченко' ---");
        string repAuthor = PoemReports.ReportByAuthor(collection, "Тарас Шевченко");
        PoemReports.ShowOrSave(repAuthor);

        Console.WriteLine("--- Завдання 2: Звіт за темою 'Філософська' ---");
        string repTopic = PoemReports.ReportByTopic(collection, "Філософська");
        PoemReports.ShowOrSave(repTopic);

        Console.WriteLine("--- Завдання 3: Звіт за словом 'небо' ---");
        string repWord = PoemReports.ReportByWord(collection, "небо");
        PoemReports.ShowOrSave(repWord);

        Console.WriteLine("--- Завдання 3: Звіт за довжиною віршів ---");
        string repLen = PoemReports.ReportByLength(collection);
        PoemReports.ShowOrSave(repLen);

        Console.WriteLine("--- Завдання 1: Збереження та завантаження файлу 'poems_demo.json' ---");
        collection.SaveToFile("poems_demo.json");
        Console.WriteLine("[OK] Колекцію збережено у 'poems_demo.json'.");
        collection.LoadFromFile("poems_demo.json");
        Console.WriteLine($"[OK] Колекцію завантажено з файлу. Всього віршів: {collection.Count}");
        File.Delete("poems_demo.json");
    }
}

