namespace Delegate;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 27.10 Делегати ===");
            Console.WriteLine("1 — Завдання 2: дії через Action, Predicate, Func");
            Console.WriteLine("2 — Завдання 3: кредитна картка з подіями");
            Console.WriteLine("0 — Вихід");
            Console.Write("Виберіть пункт: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    RunTask2();
                    break;
                case "2":
                    RunTask3();
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

    // --- Завдання 2 ---
    private static void RunTask2()
    {
        Console.WriteLine("--- Завдання 2: Action, Predicate, Func ---\n");

        // Метод, який виконує делегат Action
        RunAction("Action: DisplayTime", DelegatesTask2.DisplayTime);
        RunAction("Action: DisplayDate", DelegatesTask2.DisplayDate);
        RunAction("Action: DisplayDayOfWeek", DelegatesTask2.DisplayDayOfWeek);

        // Predicate<DateTime>: перевірка різних дат
        Predicate<DateTime> isWeekend = DelegatesTask2.IsWeekend;
        Console.WriteLine($"\nPredicate IsWeekend (сьогодні) — {isWeekend(DateTime.Today)}");
        Console.WriteLine($"Predicate IsWeekend (28.11.2026, суббота) — {isWeekend(new DateTime(2026, 11, 28))}");
        Console.WriteLine($"Predicate IsWeekend (25.11.2026, середа) — {isWeekend(new DateTime(2026, 11, 25))}");

        // Func: площі фігур
        Func<double, double, double> triangleArea = DelegatesTask2.TriangleArea;
        Func<double, double, double> rectangleArea = DelegatesTask2.RectangleArea;

        Console.WriteLine();
        Console.WriteLine($"Func TriangleArea (основа 10, висота 4) = {triangleArea(10, 4)}");
        Console.WriteLine($"Func TriangleArea (основа 7.2, висота 3.5) = {triangleArea(7.2, 3.5)}");
        Console.WriteLine($"Func RectangleArea (ширина 3.5, висота 4) = {rectangleArea(3.5, 4)}");
    }

    private static void RunAction(string title, Action action) // делегат Action як параметр
    {
        Console.WriteLine(title);
        action();
        Console.WriteLine();
    }

    // --- Завдання 3 ---
    private static void RunTask3()
    {
        Console.WriteLine("--- Завдання 3: кредитна картка ---\n");

        var card = new CreditCard(
            "4141 8888 1234 5678",
            "Шевченко Тарас Григорович",
            new DateTime(2028, 12, 31),
            "1234",
            creditLimit: 1000m,
            balance: 500m);

        // Підписка на всі п'ять подій (мультикаст: +=)
        card.Deposited += OnDeposited;
        card.Withdrawn += OnWithdrawn;
        card.CreditStarted += OnCreditStarted;
        card.CreditLimitReached += OnCreditLimitReached;
        card.PinChanged += OnPinChanged;

        Console.WriteLine("Підписані події: Поповнення, Витрата, Старт кредиту, Досягнення ліміту, Зміна PIN.\n");

        Console.WriteLine(">> Поповнення 250:");
        card.Deposit(250m);

        Console.WriteLine("\n>> Витрата 900 (йдемо в кредит):");
        card.Withdraw(900m);

        Console.WriteLine("\n>> Спроба витратити 901 (понад залишок власних + кредитних):");
        if (!card.Withdraw(901m))
        {
            Console.WriteLine("Відмовлено: сума перевищує власні + кредитні кошти.");
        }

        Console.WriteLine("\n>> Витрата 850 (досягаємо кредитного ліміту):");
        card.Withdraw(850m);

        Console.WriteLine("\n>> Спроба зняти ще 1:");
        if (!card.Withdraw(1m))
        {
            Console.WriteLine("Відмовлено: кредитний ліміт вичерпано.");
        }

        Console.WriteLine("\n>> Спроба змінити PIN з невірним старим PIN:");
        if (!card.ChangePin("9999", "5678"))
        {
            Console.WriteLine("Відмовлено: старий PIN невірний.");
        }

        Console.WriteLine("\n>> Зміна PIN з вірним старим PIN:");
        card.ChangePin("1234", "5678");

        Console.WriteLine("\nСтан картки: баланс " + card.Balance + ", ліміт " + card.CreditLimit);
    }

    private static void OnDeposited(object? sender, CardOperationEventArgs e)
        => Console.WriteLine($"[Подія] Поповнення рахунку: +{e.Amount}, баланс: {e.BalanceAfter}");

    private static void OnWithdrawn(object? sender, CardOperationEventArgs e)
        => Console.WriteLine($"[Подія] Витрата коштів: -{e.Amount}, баланс: {e.BalanceAfter}");

    private static void OnCreditStarted(object? sender, EventArgs e)
        => Console.WriteLine("[Подія] Старт використання кредитних коштів!");

    private static void OnCreditLimitReached(object? sender, EventArgs e)
        => Console.WriteLine("[Подія] Досягнуто кредитний ліміт!");

    private static void OnPinChanged(object? sender, PinChangedEventArgs e)
        => Console.WriteLine($"[Подія] PIN змінено: старий PIN більше не працює.");
}
