namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("   Класворк 10.11: Керування пам'яттю та Garbage Collector (.NET)");
            Console.WriteLine("==========================================================");
            Console.WriteLine("1 — Завдання 1: Симуляція коду DataProcessor та аналіз пам'яті");
            Console.WriteLine("2 — Завдання 2: Робота з GC (GetTotalMemory, Collect, GetGeneration, тощо)");
            Console.WriteLine("3 — Завдання 3: Практичний експеримент (10 000 об'єктів по 100 KB)");
            Console.WriteLine("4 — Виконати всі завдання підряд");
            Console.WriteLine("0 — Вихід");
            Console.Write("Виберіть пункт меню: ");

            string? choice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Task1MemoryAnalysis.Run();
                    break;
                case "2":
                    Task2GcDemo.Run();
                    break;
                case "3":
                    Task3Experiment.Run();
                    break;
                case "4":
                    Task1MemoryAnalysis.Run();
                    Console.WriteLine();
                    Task2GcDemo.Run();
                    Console.WriteLine();
                    Task3Experiment.Run();
                    break;
                case "0":
                    Console.WriteLine("Роботу завершено. До побачення!");
                    return;
                default:
                    Console.WriteLine("Невірний пункт меню. Спробуйте ще раз.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Натисніть Enter, щоб повернутися до меню...");
            Console.ReadLine();
            Console.WriteLine();
        }
    }
}

