using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Паспорт за замовчуванням ===");
        ForeignPassport defaultPassport = new ForeignPassport();
        Console.WriteLine(defaultPassport);

        Console.WriteLine();
        Console.WriteLine("=== Паспорт, створений з параметрами ===");
        TryCreate("коректні дані", () => new ForeignPassport(
            "FE123456",
            "Шевченко Тарас Григорович",
            new DateTime(1814, 3, 9),
            new DateTime(2020, 5, 20),
            new DateTime(2030, 5, 20),
            "Державна міграційна служба України",
            "UKR"));

        Console.WriteLine();
        Console.WriteLine("=== Перевірка некоректних даних ===");
        TryCreate("порожнє ПІБ", () => new ForeignPassport(
            "FE123456", "   ", new DateTime(1990, 1, 1),
            new DateTime(2020, 1, 1), new DateTime(2030, 1, 1),
            "Державна міграційна служба України", "UKR"));

        TryCreate("ПІБ з цифрами", () => new ForeignPassport(
            "FE123456", "Іваненко Іван 123", new DateTime(1990, 1, 1),
            new DateTime(2020, 1, 1), new DateTime(2030, 1, 1),
            "Державна міграційна служба України", "UKR"));

        TryCreate("неправильний формат номера", () => new ForeignPassport(
            "1234", "Іваненко Іван Іванович", new DateTime(1990, 1, 1),
            new DateTime(2020, 1, 1), new DateTime(2030, 1, 1),
            "Державна міграційна служба України", "UKR"));

        TryCreate("дата народження пізніша за дату видачі", () => new ForeignPassport(
            "FE123456", "Іваненко Іван Іванович", new DateTime(2025, 1, 1),
            new DateTime(2020, 1, 1), new DateTime(2030, 1, 1),
            "Державна міграційна служба України", "UKR"));

        TryCreate("термін дії закінчується раніше видачі", () => new ForeignPassport(
            "FE123456", "Іваненко Іван Іванович", new DateTime(1990, 1, 1),
            new DateTime(2020, 1, 1), new DateTime(2019, 1, 1),
            "Державна міграційна служба України", "UKR"));

        TryCreate("термін дії перевищує 10 років", () => new ForeignPassport(
            "FE123456", "Іваненко Іван Іванович", new DateTime(1990, 1, 1),
            new DateTime(2020, 1, 1), new DateTime(2035, 1, 1),
            "Державна міграційна служба України", "UKR"));

        TryCreate("неприпустимий код країни", () => new ForeignPassport(
            "FE123456", "Іваненко Іван Іванович", new DateTime(1990, 1, 1),
            new DateTime(2020, 1, 1), new DateTime(2030, 1, 1),
            "Державна міграційна служба України", "XXX"));

        Console.WriteLine();
        Console.WriteLine($"Усього створено об'єктів ForeignPassport: {ForeignPassport.CreatedCount}");
        Console.WriteLine($"Максимальний термін дії: {ForeignPassport.MaxValidityYears} років");
        Console.WriteLine($"Максимальна довжина номера: {ForeignPassport.MaxPassportNumberLength} символів");
    }

    static void TryCreate(string description, Func<ForeignPassport> factory)
    {
        Console.WriteLine($"--- {description} ---");
        try
        {
            ForeignPassport passport = factory();
            Console.WriteLine(passport);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"ArgumentNullException: {ex.Message}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"ArgumentOutOfRangeException: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"ArgumentException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
        }
    }
}
