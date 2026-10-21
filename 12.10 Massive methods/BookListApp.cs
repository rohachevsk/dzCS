using System;

public static class BookListApp
{
    public static void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        BookList list = new BookList();
        bool work = true;

        while (work)
        {
            ShowMenu();
            Console.Write("Оберіть дію: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                list = AddBook(list);
            }
            else if (choice == "2")
            {
                list = RemoveBook(list);
            }
            else if (choice == "3")
            {
                CheckBook(list);
            }
            else if (choice == "4")
            {
                ShowBooks(list);
            }
            else if (choice == "5")
            {
                work = false;
            }
            else
            {
                Console.WriteLine("Невірний вибір.");
            }

            Console.WriteLine();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("СПИСОК КНИГ ДО ПРОЧИТАННЯ");
        Console.WriteLine("1. Додати книгу");
        Console.WriteLine("2. Видалити книгу");
        Console.WriteLine("3. Перевірити, чи є книга у списку");
        Console.WriteLine("4. Показати всі книги");
        Console.WriteLine("5. Вийти");
    }

    static BookList AddBook(BookList list)
    {
        Console.Write("Введіть назву книги: ");
        string title = Console.ReadLine() ?? "";

        if (list[title])
        {
            Console.WriteLine("Така книга вже є у списку.");
            return list;
        }

        Console.Write("Введіть автора: ");
        string author = Console.ReadLine() ?? "";

        Console.Write("Введіть рік видання: ");
        int year;
        int.TryParse(Console.ReadLine(), out year);

        BookList result = list + new Book(title, author, year);
        Console.WriteLine("Книгу додано!");
        return result;
    }

    static BookList RemoveBook(BookList list)
    {
        Console.Write("Введіть назву книги для видалення: ");
        string title = Console.ReadLine() ?? "";

        Book? book = list.Find(title);

        if (book == null)
        {
            Console.WriteLine("Такої книги немає у списку.");
            return list;
        }

        BookList result = list - book;
        Console.WriteLine("Книгу видалено!");
        return result;
    }

    static void CheckBook(BookList list)
    {
        Console.Write("Введіть назву книги для пошуку: ");
        string title = Console.ReadLine() ?? "";

        if (list[title])
        {
            Console.WriteLine("Книга є у списку.");
        }
        else
        {
            Console.WriteLine("Книги немає у списку.");
        }
    }

    static void ShowBooks(BookList list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + list[i]);
        }
    }
}
