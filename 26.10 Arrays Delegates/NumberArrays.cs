// Набір методів для роботи з масивами.
// Кожен метод повертає новий масив і не змінює вхідний.
public static class NumberArrays
{
    // Усі парні числа.
    public static int[] GetEven(int[] array)
    {
        return Filter(array, number => number % 2 == 0);
    }

    // Усі непарні числа.
    public static int[] GetOdd(int[] array)
    {
        return Filter(array, number => number % 2 != 0);
    }

    // Усі прості числа.
    public static int[] GetPrime(int[] array)
    {
        return Filter(array, IsPrime);
    }

    // Усі числа Фібоначчі.
    public static int[] GetFibonacci(int[] array)
    {
        return Filter(array, IsFibonacci);
    }

    // Просте число: 2, 3, 5, 7, 11...  Одиниця і від'ємні не є простими.
    public static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        if (number % 2 == 0)
        {
            return number == 2;
        }

        for (int divisor = 3; (long)divisor * divisor <= number; divisor += 2)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }

    // Число Фібоначчі: 0, 1, 2, 3, 5, 8, 13, 21...
    public static bool IsFibonacci(int number)
    {
        if (number < 0)
        {
            return false;
        }

        long previous = 0;
        long current = 1;

        while (previous < number)
        {
            long next = previous + current;
            previous = current;
            current = next;
        }

        return previous == number;
    }

    private static int[] Filter(int[] array, Func<int, bool> condition)
    {
        List<int> result = new List<int>();

        foreach (int number in array)
        {
            if (condition(number))
            {
                result.Add(number);
            }
        }

        return result.ToArray();
    }
}
