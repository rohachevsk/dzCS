namespace Primer;

using System;

public class Poem
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;

    public int LinesCount => string.IsNullOrEmpty(Text)
        ? 0
        : Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Length;

    public int Length => Text.Length;

    public Poem() { }

    public Poem(string title, string author, int year, string text, string topic)
    {
        Title = title;
        Author = author;
        Year = year;
        Text = text;
        Topic = topic;
    }

    public override string ToString()
    {
        return $"\"{Title}\" — {Author} ({Year}), тема: {Topic}, символів: {Length}, рядків: {LinesCount}";
    }

    public string GetFullCard()
    {
        return $"==================================================\n" +
               $"Назва:  {Title}\n" +
               $"Автор:  {Author}\n" +
               $"Рік:    {Year}\n" +
               $"Тема:   {Topic}\n" +
               $"Розмір: {LinesCount} рядків ({Length} символів)\n" +
               $"------------------ ТЕКСТ ВІРША ------------------\n" +
               $"{Text.Trim()}\n" +
               $"==================================================";
    }
}

