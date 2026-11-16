namespace Primer;

using System;

public class Play : IDisposable
{
    private bool disposed = false;

    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }

    public Play(string title, string author, string genre, int year)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Year = year;
        Console.WriteLine($"[Конструктор] П'єсу \"{Title}\" створено.");
    }

    public void PrintInfo()
    {
        Console.WriteLine($"П'єса: \"{Title}\" | Автор: {Author} | Жанр: {Genre} | Рік: {Year}");
    }

    public override string ToString()
    {
        return $"\"{Title}\" ({Author}, {Genre}, {Year} р.)";
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Console.WriteLine($"[Dispose] Звільнення керованих ресурсів п'єси \"{Title}\".");
            }
            Console.WriteLine($"[Dispose/Finalize] Очищення пам'яті п'єси \"{Title}\".");
            disposed = true;
        }
    }

    ~Play()
    {
        Console.WriteLine($"[Деструктор ~Play()] Викликано деструктор для п'єси \"{Title}\".");
        Dispose(false);
    }
}
