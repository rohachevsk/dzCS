using System;

public static class ReverseWords
{
    public static string ReverseEachWord(string sentence)
    {
        if (sentence == null)
        {
            throw new ArgumentNullException(nameof(sentence), "Речення не може бути null.");
        }

        string[] words = sentence.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            char[] letters = words[i].ToCharArray();
            Array.Reverse(letters);
            words[i] = new string(letters);
        }

        return string.Join(" ", words);
    }
}
