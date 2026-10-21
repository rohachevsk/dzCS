class Third
{
    public static void Run()
    {
        Console.Write("Введіть речення: ");
        string sentence = Console.ReadLine() ?? "";

        string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int maxLength = 0;

        foreach (string word in words)
        {
            if (word.Length > maxLength)
            {
                maxLength = word.Length;
            }
        }

        Console.WriteLine("Результат:");
        int count = 0;

        foreach (string word in words)
        {
            if (word.Length == maxLength)
            {
                Console.WriteLine(word);
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("Нічого не знайдено");
        }
        else
        {
            Console.WriteLine("Довжина: " + maxLength);
        }

        Console.WriteLine("Натисніть Enter, щоб завершити...");
        Console.ReadLine();
    }
}
