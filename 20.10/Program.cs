using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Завдання 1. Співробітник ===");

        Employee first = new Employee("Іван", 10000);
        Employee second = new Employee("Петро", 12000);

        Console.WriteLine(first);
        Console.WriteLine(second);

        Employee more = first + 3000;
        Employee less = second - 4000;
        Console.WriteLine("first + 3000 = " + more);
        Console.WriteLine("second - 4000 = " + less);

        first++;
        Console.WriteLine("first++ = " + first);
        first--;
        Console.WriteLine("first-- = " + first);

        Employee poor = new Employee("Бідний", 200);
        poor--;
        Console.WriteLine("200 - 500 (у мінус не йдемо) = " + poor);

        Console.WriteLine("first == second: " + (first == second));
        Console.WriteLine("first != second: " + (first != second));
        Console.WriteLine("first < second: " + (first < second));
        Console.WriteLine("second > first: " + (second > first));
        Console.WriteLine("less.Equals(new Employee(\"Петро\", 8000)): " + less.Equals(new Employee("Петро", 8000)));

        Console.WriteLine();
        Console.WriteLine("=== Завдання 2. Матриця ===");

        Matrix a = new Matrix(2, 2);
        a[0, 0] = 1;
        a[0, 1] = 2;
        a[1, 0] = 3;
        a[1, 1] = 4;

        Matrix b = new Matrix(2, 2);
        b[0, 0] = 5;
        b[0, 1] = 6;
        b[1, 0] = 7;
        b[1, 1] = 8;

        Console.WriteLine("a:");
        Console.WriteLine(a);
        Console.WriteLine("b:");
        Console.WriteLine(b);

        Console.WriteLine("a + b:");
        Console.WriteLine(a + b);

        Console.WriteLine("a - b:");
        Console.WriteLine(a - b);

        Console.WriteLine("a * b:");
        Console.WriteLine(a * b);

        Console.WriteLine("a * 2:");
        Console.WriteLine(a * 2);

        Matrix c = new Matrix(2, 2);
        c[0, 0] = 1;
        c[0, 1] = 2;
        c[1, 0] = 3;
        c[1, 1] = 4;

        Console.WriteLine("a == b: " + (a == b));
        Console.WriteLine("a != b: " + (a != b));
        Console.WriteLine("a == c (однакові елементи): " + (a == c));
        Console.WriteLine("a.Equals(b): " + a.Equals(b));

        Console.WriteLine("Натисніть Enter, щоб завершити...");
        Console.ReadLine();
    }
}
