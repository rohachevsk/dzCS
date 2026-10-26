using System.IO;

// Меню програми: вводимо масив, потім обираємо спосіб обробки.
// Вибір пункту викликає делегат з масиву: dg[choice - 1](array).
public class ArrayMenu
{
    private static readonly string[] Titles = { "Парні", "Непарні", "Прості", "Фібоначчі" };

    private readonly TextReader input;
    private readonly TextWriter output;
    private readonly ArrayMethod[] methods;

    public ArrayMenu(TextReader input, TextWriter output)
    {
        this.input = input;
        this.output = output;

        methods = new ArrayMethod[]
        {
            NumberArrays.GetEven,
            NumberArrays.GetOdd,
            NumberArrays.GetPrime,
            NumberArrays.GetFibonacci
        };
    }

    public void Run()
    {
        int[]? array = ReadArray();

        if (array == null)
        {
            return;
        }

        output.WriteLine("Масив: " + string.Join(" ", array));

        while (true)
        {
            ShowMenu();

            string? line = input.ReadLine();

            if (line == null)
            {
                return;
            }

            if (!int.TryParse(line.Trim(), out int choice))
            {
                output.WriteLine("Невірний вибір.");
                continue;
            }

            if (choice == 0)
            {
                return;
            }

            if (choice < 1 || choice > methods.Length)
            {
                output.WriteLine("Невірний вибір.");
                continue;
            }

            ShowResult(choice, methods[choice - 1](array));
        }
    }

    private int[]? ReadArray()
    {
        while (true)
        {
            output.Write("Введіть числа через пробіл: ");

            string? line = input.ReadLine();

            if (line == null)
            {
                return null;
            }

            if (TryParseNumbers(line, out int[] numbers))
            {
                return numbers;
            }

            output.WriteLine("Невірні числа. Спробуйте ще раз.");
        }
    }

    private static bool TryParseNumbers(string line, out int[] numbers)
    {
        string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        numbers = new int[parts.Length];

        if (parts.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out numbers[i]))
            {
                return false;
            }
        }

        return true;
    }

    private void ShowMenu()
    {
        output.WriteLine();

        for (int i = 1; i <= Titles.Length; i++)
        {
            output.WriteLine(i + " " + Titles[i - 1]);
        }

        output.WriteLine("0 Вихід");
        output.Write("Ваш вибір: ");
    }

    private void ShowResult(int choice, int[] result)
    {
        if (result.Length == 0)
        {
            output.WriteLine("Таких чисел немає.");
            return;
        }

        output.WriteLine(Titles[choice - 1] + ": " + string.Join(" ", result));
    }
}
