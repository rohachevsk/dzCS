namespace Primer;

/// <summary>Методи розширення для string (Завдання 1).</summary>
public static class StringExtensions
{
    /// <summary>Для кожної відкриваючої дужки — відповідна закриваюча.</summary>
    private static readonly Dictionary<char, char> CloseForOpen = new()
    {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
    };

    /// <summary>
    /// Перевіряє, чи дужки в рядку розставлені правильно: кожна відкрита дужка
    /// має закриватися дужкою того ж типу і в правильному порядку.
    /// Символи, крім дужок, ігноруються. Порожній рядок і null — валідні.
    /// </summary>
    public static bool IsValidBrackets(this string? text)
    {
        Stack<char> openBrackets = new();

        foreach (char symbol in text ?? string.Empty)
        {
            if (CloseForOpen.ContainsKey(symbol))          // відкриваюча дужка
            {
                openBrackets.Push(symbol);
            }
            else if (symbol is ')' or ']' or '}')          // закриваюча дужка
            {
                if (openBrackets.Count == 0)
                {
                    return false;                          // закривати нічим
                }

                if (CloseForOpen[openBrackets.Pop()] != symbol)
                {
                    return false;                          // інший тип дужки
                }
            }
            // інші символи на валідність не впливають
        }

        return openBrackets.Count == 0;                    // усі дужки мають бути закриті
    }
}
