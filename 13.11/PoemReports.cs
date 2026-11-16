namespace Primer;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class PoemReports
{
    public static string ReportByTitle(PoemCollection collection, string titlePart)
    {
        var found = collection.SearchByTitle(titlePart);
        StringBuilder sb = new();
        sb.AppendLine($"ЗВІТ ЗА НАЗВОЮ: \"{titlePart}\" (знайдено {found.Count})");
        sb.AppendLine(new string('=', 60));
        foreach (var p in found)
        {
            sb.AppendLine(p.GetFullCard());
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string ReportByAuthor(PoemCollection collection, string authorPart)
    {
        var found = collection.SearchByAuthor(authorPart);
        StringBuilder sb = new();
        sb.AppendLine($"ЗВІТ ЗА АВТОРОМ: \"{authorPart}\" (знайдено {found.Count})");
        sb.AppendLine(new string('=', 60));
        foreach (var p in found)
        {
            sb.AppendLine($"- \"{p.Title}\" ({p.Year}), тема: {p.Topic}, рядків: {p.LinesCount}");
        }
        return sb.ToString();
    }

    public static string ReportByTopic(PoemCollection collection, string topicPart)
    {
        var found = collection.SearchByTopic(topicPart);
        StringBuilder sb = new();
        sb.AppendLine($"ЗВІТ ЗА ТЕМОЮ: \"{topicPart}\" (знайдено {found.Count})");
        sb.AppendLine(new string('=', 60));
        foreach (var p in found)
        {
            sb.AppendLine($"- \"{p.Title}\" | Автор: {p.Author} ({p.Year})");
        }
        return sb.ToString();
    }

    public static string ReportByWord(PoemCollection collection, string word)
    {
        var found = collection.SearchByWordInText(word);
        StringBuilder sb = new();
        sb.AppendLine($"ЗВІТ ЗА СЛОВОМ У ТЕКСТІ: \"{word}\" (знайдено {found.Count})");
        sb.AppendLine(new string('=', 60));
        foreach (var p in found)
        {
            sb.AppendLine($"Вірш: \"{p.Title}\" ({p.Author})");
            string[] lines = p.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.Contains(word, StringComparison.OrdinalIgnoreCase))
                {
                    sb.AppendLine($"   -> {line.Trim()}");
                }
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public static string ReportByYear(PoemCollection collection, int year)
    {
        var found = collection.SearchByYear(year);
        StringBuilder sb = new();
        sb.AppendLine($"ЗВІТ ЗА РОКОМ НАПИСАННЯ: {year} (знайдено {found.Count})");
        sb.AppendLine(new string('=', 60));
        foreach (var p in found)
        {
            sb.AppendLine($"- \"{p.Title}\" — {p.Author}, тема: {p.Topic}");
        }
        return sb.ToString();
    }

    public static string ReportByLength(PoemCollection collection)
    {
        var sorted = collection.Poems.OrderByDescending(p => p.Length).ToList();
        StringBuilder sb = new();
        sb.AppendLine("ЗВІТ ЗА ДОВЖИНОЮ ВІРШІВ (від найдовшого до найкоротшого)");
        sb.AppendLine(new string('=', 60));
        int rank = 1;
        foreach (var p in sorted)
        {
            sb.AppendLine($"{rank++}. \"{p.Title}\" ({p.Author}) — {p.Length} симв., {p.LinesCount} рядків");
        }
        return sb.ToString();
    }

    public static void ShowOrSave(string reportText, string? savePath = null)
    {
        Console.WriteLine();
        Console.WriteLine(reportText);

        if (!string.IsNullOrWhiteSpace(savePath))
        {
            File.WriteAllText(savePath, reportText);
            Console.WriteLine($"[Успіх] Звіт збережено у файл: {savePath}");
        }
    }
}
