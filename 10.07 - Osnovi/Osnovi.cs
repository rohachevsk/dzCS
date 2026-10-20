using System;
using System.Collections.Generic;

public abstract class Shape
{
    public abstract string Name { get; }

    public abstract void Draw();
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public override string Name
    {
        get
        {
            return "Коло";
        }
    }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override void Draw()
    {
        Console.WriteLine($"Малюємо {Name} із радіусом {Radius}.");
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public override string Name
    {
        get
        {
            return "Прямокутник";
        }
    }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public override void Draw()
    {
        Console.WriteLine($"Малюємо {Name} розміром {Width}x{Height}.");
    }
}

public class Triangle : Shape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }

    public override string Name
    {
        get
        {
            return "Трикутник";
        }
    }

    public Triangle(double sideA, double sideB, double sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public override void Draw()
    {
        Console.WriteLine($"Малюємо {Name} зі сторонами {SideA}, {SideB}, {SideC}.");
    }
}

public class Painter
{
    public void DrawShape(Shape shape)
    {
        if (shape == null)
        {
            throw new ArgumentNullException(nameof(shape), "Фігура не може бути null.");
        }

        Console.Write($"[Painter]: ");
        shape.Draw();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Painter painter = new Painter();

        Shape circle = new Circle(5.5);
        Shape rectangle = new Rectangle(10, 20);
        Shape triangle = new Triangle(3, 4, 5);

        painter.DrawShape(circle);
        painter.DrawShape(rectangle);
        painter.DrawShape(triangle);

        Shape[] shapes = [
            new Circle(12.0),
            new Rectangle(4, 8),
            new Triangle(6, 8, 10)
         ];

        foreach (var shape in shapes)
        {
            painter.DrawShape(shape);
        }

        int[] first = { 5, 1, 2, 2, 3, 4 };
        int[] second = { 2, 3, 3, 6, 1 };
        int[] common = CommonElements.GetCommon(first, second);
        Console.WriteLine("Спільні елементи без повторень: " + string.Join(", ", common));

        Console.WriteLine("Завдання 7. Введіть речення для перевороту слів:");
        string sentence = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Після перевороту: " + ReverseWords.ReverseEachWord(sentence));

        Console.WriteLine("Завдання 8. Введіть речення для підрахунку голосних:");
        string text = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Кількість голосних: " + VowelCounter.CountVowels(text));
    }
}