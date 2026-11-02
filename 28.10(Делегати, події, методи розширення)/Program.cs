namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 28.10 Делегати, події, методи розширення ===");
            Console.WriteLine("1 — Завдання 1: метод розширення для перевірки дужок");
            Console.WriteLine("2 — Завдання 2: кредитна картка з подіями");
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
        Console.WriteLine("Перевірка методу розширення IsValidBrackets:");
        Console.WriteLine();

        (string? Input, bool Expected)[] testCases =
        {
            ("{}[]", true),
            ("(())", true),
            ("[{}]", true),
            ("[}", false),
            ("[[{]}]", false),
            ("", true),
            (null, true),
            ("(", false),
            (")(", false),
            ("([)]", false),
            ("({[]})", true),
            ("({[}])", false),
            ("Привіт (світ) [ок] {done}", true),
        };

        foreach ((string? input, bool expected) in testCases)
        {
            bool actual = input.IsValidBrackets();
            string status = actual == expected ? "OK" : "ПОМИЛКА";
            Console.WriteLine($"\"{input ?? "null"}\" -> {actual} (очікувалось: {expected}) — {status}");
        }

        Console.WriteLine();
        Console.Write("Введіть власний рядок для перевірки: ");
        string? own = Console.ReadLine();
        Console.WriteLine(own.IsValidBrackets() ? "Валідний рядок" : "Невалідний рядок");
    }

    private static void RunTask2()
    {
        CreditCard card = new CreditCard(
            cardNumber: "4321 1234 5678 9012",
            owner: "Шевченко Тарас Григорович",
            expiryDate: new DateTime(2029, 3, 31),
            pin: "1234",
            creditLimit: 1000,
            alertSum: -500);

        card.Deposited += (sender, e) =>
            Console.WriteLine($"  [ПОДІЯ] Поповнення: +{e.Amount}, баланс: {e.BalanceAfter}");
        card.Withdrawn += (sender, e) =>
            Console.WriteLine($"  [ПОДІЯ] Витрата: -{e.Amount}, баланс: {e.BalanceAfter}");
        card.CreditStarted += (sender, e) =>
            Console.WriteLine($"  [ПОДІЯ] Старт використання кредитних коштів! Баланс: {e.BalanceAfter}");
        card.LimitReached += (sender, e) =>
            Console.WriteLine($"  [ПОДІЯ] Досягнуто заданої суми {e.AlertSum}! Баланс: {e.BalanceAfter}");
        card.PinChanged += (sender, e) =>
            Console.WriteLine($"  [ПОДІЯ] PIN змінено: {e.OldPin} -> {e.NewPin}");

        Console.WriteLine(card);
        Console.WriteLine();

        Console.WriteLine("== Поповнення 500 ==");
        card.Deposit(500);

        Console.WriteLine("== Витрата 200 (правильний PIN) ==");
        card.Withdraw(200, "1234");

        Console.WriteLine("== Витрата 500 (баланс уперше став від'ємним) ==");
        card.Withdraw(500, "1234");

        Console.WriteLine("== Витрата 350 (баланс нижче -500) ==");
        card.Withdraw(350, "1234");

        Console.WriteLine("== Спроба витратити 1200 (за кредитний ліміт) ==");
        bool ok = card.Withdraw(1200, "1234");
        Console.WriteLine(ok ? "Операція пройшла" : "Відмовлено: недостатньо коштів (кредитний ліміт)");

        Console.WriteLine("== Зміна PIN із неправильним старим PIN ==");
        ok = card.ChangePin("9999", "4321");
        Console.WriteLine(ok ? "PIN змінено" : "Відмовлено: неправильний старий PIN");

        Console.WriteLine("== Зміна PIN із правильним старим PIN ==");
        ok = card.ChangePin("1234", "4321");
        Console.WriteLine(ok ? "PIN змінено" : "Відмовлено: неправильний старий PIN");
    }
}
