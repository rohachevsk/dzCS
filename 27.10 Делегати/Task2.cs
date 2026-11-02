using System.Globalization;

namespace Delegate;

/// <summary>
/// Завдання 2: набір методів, реалізованих через делегати Action, Predicate і Func.
/// </summary>
public static class DelegatesTask2
{
    /// <summary>Відображення поточного часу.</summary>
    public static readonly Action DisplayTime = () =>
        Console.WriteLine($"Поточний час: {DateTime.Now:HH:mm:ss}");

    /// <summary>Відображення поточної дати.</summary>
    public static readonly Action DisplayDate = () =>
        Console.WriteLine($"Поточна дата: {DateTime.Now:dd.MM.yyyy}");

    /// <summary>Відображення поточного дня тижня (назва українською).</summary>
    public static readonly Action DisplayDayOfWeek = () =>
        Console.WriteLine(
            $"Поточний день тижня: {DateTime.Now.ToString("dddd", new CultureInfo("uk-UA"))}");

    /// <summary>Перевірка: чи є дата вихідним днем.</summary>
    public static readonly Predicate<DateTime> IsWeekend = date =>
        date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>Площа трикутника за основою і висотою.</summary>
    public static readonly Func<double, double, double> TriangleArea = (@base, height) =>
        0.5 * @base * height;

    /// <summary>Площа прямокутника.</summary>
    public static readonly Func<double, double, double> RectangleArea = (width, height) =>
        width * height;
}
