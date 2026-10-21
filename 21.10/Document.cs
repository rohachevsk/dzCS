using System;

public abstract class Document : ITextProcessor
{
    private string title;
    private string author;
    private string text;
    private DateTime createdAt;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Author
    {
        get { return author; }
        set { author = value; }
    }

    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    public DateTime CreatedAt
    {
        get { return createdAt; }
    }

    public Document(string title, string author, string text)
    {
        this.title = title;
        this.author = author;
        this.text = text;
        this.createdAt = DateTime.Now;
    }

    public virtual string GetInfo()
    {
        return "Назва: " + Title + Environment.NewLine +
               "Автор: " + Author + Environment.NewLine +
               "Створено: " + CreatedAt.ToString("dd.MM.yyyy HH:mm");
    }

    public string ProcessText()
    {
        string[] words = SplitWords(Text);
        return string.Join(" ", words);
    }

    public int CountWords()
    {
        return SplitWords(Text).Length;
    }

    public bool ContainsWord(string word)
    {
        string[] words = SplitWords(Text);

        for (int i = 0; i < words.Length; i++)
        {
            if (string.Equals(words[i], word, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public DocumentStatistics GetStatistics()
    {
        string[] words = SplitWords(Text);
        string longestWord = "";
        int uniqueCount = 0;

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > longestWord.Length)
            {
                longestWord = words[i];
            }

            bool wasEarlier = false;

            for (int j = 0; j < i; j++)
            {
                if (string.Equals(words[i], words[j], StringComparison.OrdinalIgnoreCase))
                {
                    wasEarlier = true;
                    break;
                }
            }

            if (!wasEarlier)
            {
                uniqueCount++;
            }
        }

        return new DocumentStatistics(words.Length, Text.Length, longestWord, uniqueCount);
    }

    protected static string[] SplitWords(string text)
    {
        char[] punctuation = { '.', ',', '!', '?', ';', ':', '-', '—', '(', ')', '"', '\n', '\r', '\t' };
        string cleanText = text;

        for (int i = 0; i < punctuation.Length; i++)
        {
            cleanText = cleanText.Replace(punctuation[i], ' ');
        }

        return cleanText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
}
