using System;
using System.Collections.Generic;

public class BookList
{
    private List<Book> books = new List<Book>();

    public int Count
    {
        get { return books.Count; }
    }

    public Book this[int index]
    {
        get { return books[index]; }
        set { books[index] = value; }
    }

    public bool this[string title]
    {
        get
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (string.Equals(books[i].Title, title, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public Book? Find(string title)
    {
        for (int i = 0; i < books.Count; i++)
        {
            if (string.Equals(books[i].Title, title, StringComparison.OrdinalIgnoreCase))
            {
                return books[i];
            }
        }

        return null;
    }

    public static BookList operator +(BookList list, Book book)
    {
        BookList result = new BookList();

        for (int i = 0; i < list.Count; i++)
        {
            result.books.Add(list[i]);
        }

        result.books.Add(book);
        return result;
    }

    public static BookList operator -(BookList list, Book book)
    {
        BookList result = new BookList();

        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].Equals(book))
            {
                result.books.Add(list[i]);
            }
        }

        return result;
    }

    public static bool operator ==(BookList first, BookList second)
    {
        if (ReferenceEquals(first, second))
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        if (first.Count != second.Count)
        {
            return false;
        }

        for (int i = 0; i < first.Count; i++)
        {
            if (!first[i].Equals(second[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator !=(BookList first, BookList second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is BookList other))
        {
            return false;
        }

        return this == other;
    }

    public override int GetHashCode()
    {
        return Count;
    }
}
