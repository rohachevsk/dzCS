class Second
{
    public static void Run()
    {
        Console.Write("Завдання 2 ");
        Console.Write("Введіть речення: ");
        string sentence = Console.ReadLine() ?? "";

        Console.Write("Введіть літеру: ");
        string letter = Console.ReadLine() ?? "";

        string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string letterLower = letter.ToLower();
        int count = 0;

        foreach (string word in words)
        {
            if (word.ToLower().StartsWith(letterLower))
            {
                Console.WriteLine(word);
                count++;
            }
        }

        Console.WriteLine("Кількість: " + count);

        Console.WriteLine("Натисніть Enter, щоб завершити...");
        Console.ReadLine();
    }
}
