using System;

public class Magazine
{
    private string name = "";
    private int year;
    private string description = "";
    private string phone = "";
    private string email = "";
    private int workers;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int Year
    {
        get { return year; }
        set { year = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    public int Workers
    {
        get { return workers; }
        set
        {
            if (value < 0)
            {
                workers = 0;
            }
            else
            {
                workers = value;
            }
        }
    }

    public Magazine()
    {
        Name = "default";
        Year = 2000;
        Description = "default";
        Phone = "default";
        Email = "default";
        Workers = 0;
    }

    public Magazine(string name, int year, string description, string phone, string email, int workers)
    {
        Name = name;
        Year = year;
        Description = description;
        Phone = phone;
        Email = email;
        Workers = workers;
    }

    public void Input()
    {
        Console.Write("Назва журналу: ");
        Name = Console.ReadLine() ?? string.Empty;

        Console.Write("Рік заснування: ");
        int.TryParse(Console.ReadLine(), out int inputYear);
        Year = inputYear;

        Console.Write("Опис журналу: ");
        Description = Console.ReadLine() ?? string.Empty;

        Console.Write("Контактний телефон: ");
        Phone = Console.ReadLine() ?? string.Empty;

        Console.Write("Email: ");
        Email = Console.ReadLine() ?? string.Empty;

        Console.Write("Кількість працівників: ");
        int.TryParse(Console.ReadLine(), out int inputWorkers);
        Workers = inputWorkers;
    }

    public void Print()
    {
        Console.WriteLine("--- Журнал ---");
        Console.WriteLine("Назва: " + Name);
        Console.WriteLine("Рік заснування: " + Year);
        Console.WriteLine("Опис: " + Description);
        Console.WriteLine("Телефон: " + Phone);
        Console.WriteLine("Email: " + Email);
        Console.WriteLine("Кількість працівників: " + Workers);
    }

    public static Magazine operator +(Magazine magazine, int count)
    {
        return new Magazine(magazine.Name, magazine.Year, magazine.Description, magazine.Phone, magazine.Email, magazine.Workers + count);
    }

    public static Magazine operator -(Magazine magazine, int count)
    {
        return new Magazine(magazine.Name, magazine.Year, magazine.Description, magazine.Phone, magazine.Email, magazine.Workers - count);
    }

    public static bool operator ==(Magazine first, Magazine second)
    {
        if (ReferenceEquals(first, second))
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Workers == second.Workers;
    }

    public static bool operator !=(Magazine first, Magazine second)
    {
        return !(first == second);
    }

    public static bool operator <(Magazine first, Magazine second)
    {
        return first.Workers < second.Workers;
    }

    public static bool operator >(Magazine first, Magazine second)
    {
        return first.Workers > second.Workers;
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is Magazine other))
        {
            return false;
        }

        return Workers == other.Workers;
    }

    public override int GetHashCode()
    {
        return Workers.GetHashCode();
    }
}
