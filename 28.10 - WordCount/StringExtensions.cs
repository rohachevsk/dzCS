public static class StringExtensions
{
    /// <summary>
    /// Рахує кількість слів у рядку. Слова розділені будь-яким пробільним символом
    /// (пробіл, табуляція, перехід на новий рядок). null та порожній рядок дають 0.
    /// </summary>
    public static int CountWords(this string? text)
    {
        if (text == null)
        {
            return 0;
        }

        int count = 0;
        bool inWord = false;

        foreach (char symbol in text)
        {
            if (char.IsWhiteSpace(symbol))
            {
                inWord = false;      // пробіл: слово закінчилось
            }
            else if (inWord == false)
            {
                count++;             // перший символ нового слова
                inWord = true;
            }
        }

        return count;
    }
}
