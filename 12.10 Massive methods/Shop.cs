using System;

public class Shop
{
    private string name = "";
    private string address = "";
    private string profile = "";
    private string phone = "";
    private string email = "";
    private double area;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    public string Profile
    {
        get { return profile; }
        set { profile = value; }
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

    public double Area
    {
        get { return area; }
        set
        {
            if (value < 0)
            {
                area = 0;
            }
            else
            {
                area = value;
            }
        }
    }

    public Shop()
    {
        Name = "default";
        Address = "default";
        Profile = "default";
        Phone = "default";
        Email = "default";
        Area = 0;
    }

    public Shop(string name, string address, string profile, string phone, string email, double area)
    {
        Name = name;
        Address = address;
        Profile = profile;
        Phone = phone;
        Email = email;
        Area = area;
    }

    public void Input()
    {
        Console.Write("Назва магазину: ");
        Name = Console.ReadLine() ?? string.Empty;

        Console.Write("Адреса: ");
        Address = Console.ReadLine() ?? string.Empty;

        Console.Write("Профіль магазину: ");
        Profile = Console.ReadLine() ?? string.Empty;

        Console.Write("Контактний телефон: ");
        Phone = Console.ReadLine() ?? string.Empty;

        Console.Write("Email: ");
        Email = Console.ReadLine() ?? string.Empty;

        Console.Write("Площа магазину (кв.м): ");
        double.TryParse(Console.ReadLine(), out double inputArea);
        Area = inputArea;
    }

    public void Print()
    {
        Console.WriteLine("--- Магазин ---");
        Console.WriteLine("Назва: " + Name);
        Console.WriteLine("Адреса: " + Address);
        Console.WriteLine("Профіль: " + Profile);
        Console.WriteLine("Телефон: " + Phone);
        Console.WriteLine("Email: " + Email);
        Console.WriteLine("Площа: " + Area + " кв.м");
    }

    public static Shop operator +(Shop shop, double size)
    {
        return new Shop(shop.Name, shop.Address, shop.Profile, shop.Phone, shop.Email, shop.Area + size);
    }

    public static Shop operator -(Shop shop, double size)
    {
        return new Shop(shop.Name, shop.Address, shop.Profile, shop.Phone, shop.Email, shop.Area - size);
    }

    public static bool operator ==(Shop first, Shop second)
    {
        if (ReferenceEquals(first, second))
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        return first.Area == second.Area;
    }

    public static bool operator !=(Shop first, Shop second)
    {
        return !(first == second);
    }

    public static bool operator <(Shop first, Shop second)
    {
        return first.Area < second.Area;
    }

    public static bool operator >(Shop first, Shop second)
    {
        return first.Area > second.Area;
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is Shop other))
        {
            return false;
        }

        return Area == other.Area;
    }

    public override int GetHashCode()
    {
        return Area.GetHashCode();
    }
}
