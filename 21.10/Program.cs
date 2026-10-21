using System;
using System.Collections.Generic;

class Program
{
    static List<Document> documents = new List<Document>();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        bool work = true;

        while (work)
        {
            ShowMenu();
            Console.Write("Оберіть дію: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                AddArticle();
            }
            else if (choice == "2")
            {
                AddReport();
            }
            else if (choice == "3")
            {
                ShowAllDocuments();
            }
            else if (choice == "4")
            {
                FindByWord();
            }
            else if (choice == "5")
            {
                ShowStatistics();
            }
            else if (choice == "6")
            {
                ShowReport();
            }
            else if (choice == "7")
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
        Console.WriteLine("СИСТЕМА ОБРОБКИ ДОКУМЕНТІВ");
        Console.WriteLine("1. Додати статтю");
        Console.WriteLine("2. Додати звіт");
        Console.WriteLine("3. Показати всі документи");
        Console.WriteLine("4. Знайти документи за словом");
        Console.WriteLine("5. Показати статистику документа");
        Console.WriteLine("6. Згенерувати загальний звіт");
        Console.WriteLine("7. Вийти");
    }

    static void AddArticle()
    {
        Console.Write("Введіть назву статті: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Введіть автора: ");
        string author = Console.ReadLine() ?? "";

        Console.Write("Введіть категорію: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("Введіть час читання: ");
        int readTime = ReadNumber();

        Console.Write("Введіть текст: ");
        string text = Console.ReadLine() ?? "";

        documents.Add(new Article(title, author, text, category, readTime));
        Console.WriteLine("Документ успішно створено!");
    }

    static void AddReport()
    {
        Console.Write("Введіть назву звіту: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Введіть автора: ");
        string author = Console.ReadLine() ?? "";

        Console.Write("Введіть відділ: ");
        string department = Console.ReadLine() ?? "";

        Console.Write("Введіть кількість сторінок: ");
        int pageCount = ReadNumber();

        Console.Write("Введіть текст: ");
        string text = Console.ReadLine() ?? "";

        documents.Add(new Report(title, author, text, department, pageCount));
        Console.WriteLine("Документ успішно створено!");
    }

    static void ShowAllDocuments()
    {
        if (documents.Count == 0)
        {
            Console.WriteLine("Документів ще немає.");
            return;
        }

        for (int i = 0; i < documents.Count; i++)
        {
            Console.WriteLine("--- Документ №" + (i + 1) + " ---");
            Console.WriteLine(documents[i].GetInfo());
            Console.WriteLine();
        }

        DocumentSummary summary = CreateSummary(documents[0]);
        DocumentSummary changed = summary with { Author = "Новий автор" };

        Console.WriteLine("Короткі відомості:");
        Console.WriteLine(summary);
        Console.WriteLine("Копія через with (змінений автор):");
        Console.WriteLine(changed);
    }

    static void FindByWord()
    {
        Console.Write("Введіть слово для пошуку: ");
        string word = Console.ReadLine() ?? "";

        bool found = false;

        for (int i = 0; i < documents.Count; i++)
        {
            if (documents[i].ContainsWord(word))
            {
                Console.WriteLine("Знайдено документ:");
                Console.WriteLine(documents[i].Title);
                Console.WriteLine("Автор: " + documents[i].Author);
                Console.WriteLine("Кількість слів: " + documents[i].CountWords());
                Console.WriteLine();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Документів не знайдено.");
        }
    }

    static void ShowStatistics()
    {
        if (documents.Count == 0)
        {
            Console.WriteLine("Документів ще немає.");
            return;
        }

        for (int i = 0; i < documents.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + documents[i].Title);
        }

        Console.Write("Введіть номер документа: ");
        int number = ReadNumber();

        if (number < 1 || number > documents.Count)
        {
            Console.WriteLine("Невірний номер документа.");
            return;
        }

        Document document = documents[number - 1];
        DocumentStatistics statistics = document.GetStatistics();

        Console.WriteLine("Статистика документа \"" + document.Title + "\":");
        Console.WriteLine("Кількість слів: " + statistics.WordCount);
        Console.WriteLine("Кількість символів: " + statistics.CharacterCount);
        Console.WriteLine("Найдовше слово: " + statistics.LongestWord);
        Console.WriteLine("Унікальних слів: " + statistics.UniqueWordCount);
    }

    static void ShowReport()
    {
        if (documents.Count == 0)
        {
            Console.WriteLine("Документів ще немає.");
            return;
        }

        Console.WriteLine(ReportGenerator.GenerateReport(documents));
    }

    static DocumentSummary CreateSummary(Document document)
    {
        string documentType = "Документ";

        if (document is Article)
        {
            documentType = "Стаття";
        }
        else if (document is Report)
        {
            documentType = "Звіт";
        }

        return new DocumentSummary(document.Title, document.Author, documentType, document.CountWords());
    }

    static int ReadNumber()
    {
        string input = Console.ReadLine() ?? "";
        int number;

        if (int.TryParse(input, out number))
        {
            return number;
        }

        return 0;
    }
}
