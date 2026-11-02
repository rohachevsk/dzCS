namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 02.11 Boxing, Unboxing, collections ===");
            Console.WriteLine("1 — Завдання 1: Океанариум");
            Console.WriteLine("2 — Завдання 2: Футбольна команда");
            Console.WriteLine("0 — Вихід");
            Console.Write("Виберіть пункт: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    RunTask1();
                    break;
                case "2":
                    RunTask2();
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

    private static void RunTask1()
    {
        Oceanarium oceanarium = new Oceanarium("Морський світ");

        oceanarium.Add(new Dolphin("Афаліна", 5, 180, 6));
        oceanarium.Add(new Shark("Біла акула", 10, 700, 300));
        oceanarium.Add(new Octopus("Оскар", 3, 12, 8));
        oceanarium.Add(new SeaTurtle("Черепаха Тортила", 80, 95, 110));
        oceanarium.PrintAll();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 — Додати жителя");
            Console.WriteLine("2 — Показати повний список");
            Console.WriteLine("0 — Назад");
            Console.Write("Виберіть пункт: ");

            string? action = Console.ReadLine();
            Console.WriteLine();

            if (action == "0")
            {
                return;
            }

            switch (action)
            {
                case "1":
                    Console.Write("Ім'я: ");
                    string? name = Console.ReadLine();
                    Console.Write("Вік: ");
                    int.TryParse(Console.ReadLine(), out int age);
                    Console.Write("Вага (кг): ");
                    double.TryParse(Console.ReadLine(), out double weight);
                    oceanarium.Add(new SeaItem(name ?? "Без імені", age, weight));
                    Console.WriteLine("Додано.");
                    break;
                case "2":
                    oceanarium.PrintAll();
                    break;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }
        }
    }

    private static void RunTask2()
    {
        FootballTeam team = new FootballTeam("Динамо");

        team.AddPlayer(new Player("Бущан", 1, "Воротар"));
        team.AddPlayer(new Player("Забарний", 13, "Захисник"));
        team.AddPlayer(new Player("Шапаренко", 10, "Півзахисник"));
        team.AddPlayer(new Player("Ванік", 9, "Нападник"));
        team.PrintAll();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1 — Додати гравця");
            Console.WriteLine("2 — Показати повний список");
            Console.WriteLine("0 — Назад");
            Console.Write("Виберіть пункт: ");

            string? action = Console.ReadLine();
            Console.WriteLine();

            if (action == "0")
            {
                return;
            }

            switch (action)
            {
                case "1":
                    Console.Write("Ім'я: ");
                    string? name = Console.ReadLine();
                    Console.Write("Номер: ");
                    int.TryParse(Console.ReadLine(), out int number);
                    Console.Write("Позиція: ");
                    string? position = Console.ReadLine();
                    team.AddPlayer(new Player(name ?? "Без імені", number, position ?? "Без позиції"));
                    Console.WriteLine("Додано.");
                    break;
                case "2":
                    team.PrintAll();
                    break;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }
        }
    }
}
