class First
{
    public static void Run()
    {
        Console.Write("Введіть речення: ");
        string sentence = Console.ReadLine() ?? "";

        Console.Write("Введіть підрядок: ");
        string sub = Console.ReadLine() ?? "";

        string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string subLower = sub.ToLower();
        int count = 0;

        foreach (string word in words)
        {
            if (word.ToLower().Contains(subLower))
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
            Console.WriteLine("Знайдено: " + count);
        }

        Console.WriteLine("Натисніть Enter, щоб завершити...");
        Console.ReadLine();
    }
}
