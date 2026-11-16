namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 10.11 Деструктори та IDisposable ===");
            Console.WriteLine("1 — Завдання 1: Клас «П'єса» та робота деструктора");
            Console.WriteLine("2 — Завдання 2: Клас «Магазин» та робота IDisposable");
            Console.WriteLine("3 — Завдання 3: Комплексне тестування Dispose та деструкторів");
            Console.WriteLine("4 — Запустити всі три завдання");
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
                    DemoTask1();
                    break;
                case "2":
                    DemoTask2();
                    break;
                case "3":
                    DemoTask3();
                    break;
                case "4":
                    DemoTask1();
                    Console.WriteLine(new string('-', 60));
                    DemoTask2();
                    Console.WriteLine(new string('-', 60));
                    DemoTask3();
                    break;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void DemoTask1()
    {
        Console.WriteLine("--- Завдання 1: Клас «П'єса» та демонстрація деструктора ---");
        CreatePlayWithoutDispose();

        Console.WriteLine("\nОб'єкт п'єси вийшов за межі методу. Викликаємо збирач сміття (GC.Collect)...");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.WriteLine("Збір сміття завершено.");
    }

    private static void CreatePlayWithoutDispose()
    {
        Play play = new Play("Гамлет", "Вільям Шекспір", "Трагедія", 1601);
        play.PrintInfo();
    }

    private static void DemoTask2()
    {
        Console.WriteLine("--- Завдання 2: Клас «Магазин» та реалізація IDisposable ---");

        Console.WriteLine("\n1. Явний виклик методу Dispose():");
        Shop shop1 = new Shop("Сільпо", "вул. Хрещатик, 1", ShopType.Grocery);
        shop1.PrintInfo();
        Console.WriteLine("Викликаємо shop1.Dispose() вручну:");
        shop1.Dispose();

        Console.WriteLine("\n2. Використання конструкції using (автоматичний виклик Dispose):");
        using (Shop shop2 = new Shop("Епіцентр", "просп. Степана Бандери, 11", ShopType.Hardware))
        {
            shop2.PrintInfo();
            Console.WriteLine("Працюємо всередині блоку using...");
        }
        Console.WriteLine("Блок using завершено (Dispose викликано автоматично).");
    }

    private static void DemoTask3()
    {
        Console.WriteLine("--- Завдання 3: IDisposable + деструктор для «П'єса» та «Магазин» ---");

        Console.WriteLine("\nСценарій А: Використання через using (Dispose викликається, деструктор пригнічується):");
        using (Play play = new Play("Лісова пісня", "Леся Українка", "Драма-феєрія", 1911))
        {
            play.PrintInfo();
        }

        Console.WriteLine("\nСценарій Б: Об'єкт створено без виклику Dispose (деструктор спрацює при GC):");
        CreateObjectsToFinalize();

        Console.WriteLine("\nВикликаємо примусовий збір сміття GC.Collect()...");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.WriteLine("Збір сміття та фіналізацію завершено.");
    }

    private static void CreateObjectsToFinalize()
    {
        Play play = new Play("За двома зайцями", "Михайло Старицький", "Комедія", 1883);
        Shop shop = new Shop("Intertop", "вул. Велика Васильківська, 30", ShopType.Shoes);
        play.PrintInfo();
        shop.PrintInfo();
    }
}
