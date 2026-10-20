using System;

public static class VowelCounter
{
    private const string Vowels = "аеєиіїоуюяaeiouy";

    public static int CountVowels(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text), "Речення не може бути null.");
        }

        int count = 0;
        foreach (char symbol in text)
        {
            if (Vowels.IndexOf(char.ToLowerInvariant(symbol)) >= 0)
            {
                count++;
            }
        }

        return count;
    }
}
