namespace Primer;

public static class StringExtensions
{
    private static readonly Dictionary<char, char> CloseForOpen = new()
    {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
    };

    public static bool IsValidBrackets(this string? text)
    {
        Stack<char> openBrackets = new();

        foreach (char symbol in text ?? string.Empty)
        {
            if (CloseForOpen.ContainsKey(symbol))
            {
                openBrackets.Push(symbol);
            }
            else if (symbol is ')' or ']' or '}')
            {
                if (openBrackets.Count == 0)
                {
                    return false;
                }

                if (CloseForOpen[openBrackets.Pop()] != symbol)
                {
                    return false;
                }
            }
        }

        return openBrackets.Count == 0;
    }
}
