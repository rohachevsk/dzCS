namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 04.11 BrowserHistory (LinkedList) ===");
            Console.WriteLine("1 — Демонстрація з умови завдання");
            Console.WriteLine("2 — Інтерактивне тестування браузера");
            Console.WriteLine("0 — Вихід");
            Console.Write("Виберіть пункт: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    RunExampleDemo();
                    break;
                case "2":
                    RunInteractiveMode();
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

    private static void RunExampleDemo()
    {
        Console.WriteLine("--- Демонстрація BrowserHistory ---");
        Console.WriteLine("Створюємо браузер з домашньою сторінкою 'home.com':");
        BrowserHistory browser = new BrowserHistory("home.com");
        browser.ShowHistory();
        Console.WriteLine();

        Console.WriteLine("browser.Visits(\"google.com\");");
        browser.Visits("google.com");

        Console.WriteLine("browser.Visits(\"github.com\");");
        browser.Visits("github.com");
        browser.ShowHistory();
        Console.WriteLine();

        Console.Write("Console.WriteLine(browser.Back(1)); -> ");
        Console.WriteLine(browser.Back(1));
        browser.ShowHistory();
        Console.WriteLine();

        Console.WriteLine("browser.Visits(\"stackoverflow.com\");");
        browser.Visits("stackoverflow.com");
        browser.ShowHistory();
        Console.WriteLine();

        Console.Write("Console.WriteLine(browser.Back(2)); -> ");
        Console.WriteLine(browser.Back(2));
        browser.ShowHistory();
        Console.WriteLine();

        Console.Write("Console.WriteLine(browser.Forward(1)); -> ");
        Console.WriteLine(browser.Forward(1));
        browser.ShowHistory();
    }

    private static void RunInteractiveMode()
    {
        Console.Write("Введіть стартову сторінку (за замовчуванням 'home.com'): ");
        string? home = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(home))
        {
            home = "home.com";
        }

        BrowserHistory browser = new BrowserHistory(home);
        Console.WriteLine($"Браузер запущено. Поточна сторінка: {home}");

        while (true)
        {
            Console.WriteLine();
            browser.ShowHistory();
            Console.WriteLine("Дії: 1 — Visits(url), 2 — Back(steps), 3 — Forward(steps), 4 — GetCurrentPage(), 0 — Назад у головне меню");
            Console.Write("Ваш вибір: ");

            string? cmd = Console.ReadLine();
            Console.WriteLine();

            switch (cmd)
            {
                case "1":
                    Console.Write("Введіть URL для переходу: ");
                    string? url = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        browser.Visits(url);
                        Console.WriteLine($"Перейшли на: {browser.GetCurrentPage()}");
                    }
                    break;
                case "2":
                    Console.Write("Кількість кроків назад: ");
                    if (int.TryParse(Console.ReadLine(), out int backSteps))
                    {
                        string page = browser.Back(backSteps);
                        Console.WriteLine($"Поточна сторінка після Back({backSteps}): {page}");
                    }
                    else
                    {
                        Console.WriteLine("Некоректне число кроків.");
                    }
                    break;
                case "3":
                    Console.Write("Кількість кроків вперед: ");
                    if (int.TryParse(Console.ReadLine(), out int fwdSteps))
                    {
                        string page = browser.Forward(fwdSteps);
                        Console.WriteLine($"Поточна сторінка після Forward({fwdSteps}): {page}");
                    }
                    else
                    {
                        Console.WriteLine("Некоректне число кроків.");
                    }
                    break;
                case "4":
                    Console.WriteLine($"Поточна активна сторінка: {browser.GetCurrentPage()}");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірна команда.");
                    break;
            }
        }
    }
}

