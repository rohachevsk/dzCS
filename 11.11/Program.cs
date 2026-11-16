namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 11.11 Робота з файлами ===");
            Console.WriteLine("1 — Завдання 3: Додаток «Модератор»");
            Console.WriteLine("2 — Завдання 4: Перевертання вмісту файлу");
            Console.WriteLine("3 — Завдання 5: Аналіз 100 000 чисел");
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
                    Task3_Moderator.Run();
                    break;
                case "2":
                    Task4_FileReverser.Run();
                    break;
                case "3":
                    Task5_NumbersAnalyzer.Run();
                    break;
                case "4":
                    Task3_Moderator.Run();
                    Console.WriteLine(new string('-', 60));
                    Task4_FileReverser.Run();
                    Console.WriteLine(new string('-', 60));
                    Task5_NumbersAnalyzer.Run();
                    break;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

