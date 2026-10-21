class Fifth
{
    public static void Run()
    {
        Console.Write("Введіть речення: ");
        string sentence = Console.ReadLine() ?? "";

        Console.Write("Введіть слово для пошуку: ");
        string word = Console.ReadLine() ?? "";

        string[] words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        string wordLower = word.ToLower();
        string indexes = "";
        int count = 0;

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].ToLower() == wordLower)
            {
                if (count > 0) indexes += ", ";
                indexes += i;
                count++;
            }
        }

        if (count == 0)
        {
            Console.WriteLine("Збігів немає");
        }
        else
        {
            string raz = count == 1 ? "раз" : "рази";
            Console.WriteLine("Знайдено " + count + " " + raz);
            Console.WriteLine("Індекси: " + indexes);
        }

        Console.WriteLine("Натисніть Enter, щоб завершити...");
        Console.ReadLine();
    }
}
