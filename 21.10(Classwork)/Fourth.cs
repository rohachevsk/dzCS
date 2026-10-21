class Fourth
{
    public static void Run()
    {
        Console.Write("Введіть речення: ");
        string sentence = Console.ReadLine() ?? "";

        Console.Write("Введіть рядок для пошуку: ");
        string sub = Console.ReadLine() ?? "";

        string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string subLower = sub.ToLower();
        int count = 0;

        foreach (string word in words)
        {
            if (word.ToLower().EndsWith(subLower))
            {
                Console.WriteLine(word);
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("Збігів немає");
        }

        Console.WriteLine("Натисніть Enter, щоб завершити...");
        Console.ReadLine();
    }
}
