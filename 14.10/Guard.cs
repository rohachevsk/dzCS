using System;

internal static class Guard
{
    public static string RequireText(string? value, string paramName, string fieldName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(paramName, $"{fieldName} не може бути null.");
        }

        string text = value.Trim();
        if (text.Length == 0)
        {
            throw new ArgumentException($"{fieldName} не може бути порожнім.", paramName);
        }

        return text;
    }
}
