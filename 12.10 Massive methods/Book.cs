using System;

public class Book
{
    private string title = "";
    private string author = "";
    private int year;

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

    public int Year
    {
        get { return year; }
        set { year = value; }
    }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }

    public override string ToString()
    {
        return Title + " — " + Author + " (" + Year + ")";
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is Book other))
        {
            return false;
        }

        return string.Equals(Title, other.Title, StringComparison.OrdinalIgnoreCase) &&
               string.Equals(Author, other.Author, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return (Title.ToLower() + Author.ToLower()).GetHashCode();
    }
}
