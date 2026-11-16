namespace Primer;

using System;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 16.11 Синтаксис запитів LINQ (Query Syntax) ===");
            Console.WriteLine("1 — Завдання 1: Запити для масиву цілих чисел");
            Console.WriteLine("2 — Завдання 2: Запити для масиву рядків (міст)");
            Console.WriteLine("3 — Завдання 3: Запити для колекції студентів");
            Console.WriteLine("4 — Запустити демонстрацію всіх трьох завдань");
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
                    Task1_IntegerQueries.Run();
                    break;
                case "2":
                    Task2_StringQueries.Run();
                    break;
                case "3":
                    Task3_StudentQueries.Run();
                    break;
                case "4":
                    Task1_IntegerQueries.Run();
                    Console.WriteLine(new string('-', 60));
                    Task2_StringQueries.Run();
                    Console.WriteLine(new string('-', 60));
                    Task3_StudentQueries.Run();
                    break;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

