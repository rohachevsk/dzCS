namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 04.11 Classwork: черга в поліклініці ===");
            Console.WriteLine("1 — Запустити емуляцію черги");
            Console.WriteLine("0 — Вихід");
            Console.Write("Виберіть пункт: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    RunQueueEmulation();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void RunQueueEmulation()
    {
        Polyclinic polyclinic = new Polyclinic("Лікар Кузнєцов");

        string[] names = { "Андрій", "Оля", "Тарас", "Марія", "Іван", "Катерина", "Софія", "Богдан" };
        int nextPatient = 0;
        int served = 0;

        Console.WriteLine("Пацієнти приходять кожні 2 секунди, лікар приймає по одному кожні 4 секунди.");
        Console.WriteLine("Емуляція триває, поки лікар не прийме 5 пацієнтів.");
        Console.WriteLine();

        for (int second = 1; served < 5; second++)
        {
            Thread.Sleep(1000);

            if (second % 2 == 0 && nextPatient < names.Length)
            {
                Console.WriteLine($"[сек {second}] Прийшов пацієнт {names[nextPatient]}:");
                polyclinic.AddPatient(new Patient(names[nextPatient]));
                nextPatient++;
            }

            if (second % 4 == 0 && polyclinic.Count > 0)
            {
                Console.WriteLine($"[сек {second}] Лікар викликав наступного пацієнта:");
                polyclinic.CallNext();
                served++;
            }

            polyclinic.PrintQueue();
            Console.WriteLine();
        }

        Console.WriteLine($"Емуляцію завершено. Лікар прийняв {served} пацієнтів.");
    }
}
