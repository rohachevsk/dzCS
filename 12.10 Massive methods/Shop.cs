using System;

public class Shop
{
    private string _name;
    private string _address;
    private string _profile;
    private string _phone;
    private string _email;

    public Shop()
    {
        _name = "default";
        _address = "default";
        _profile = "default";
        _phone = "default";
        _email = "default";
    }

    public void Input()
    {
        Console.Write("Назва магазину: ");
        SetName(Console.ReadLine() ?? string.Empty);

        Console.Write("Адреса: ");
        SetAddress(Console.ReadLine() ?? string.Empty);

        Console.Write("Опис профілю магазину: ");
        SetProfile(Console.ReadLine() ?? string.Empty);

        Console.Write("Контактний телефон: ");
        SetPhone(Console.ReadLine() ?? string.Empty);

        Console.Write("Email: ");
        SetEmail(Console.ReadLine() ?? string.Empty);
    }

    public void Print()
    {
        Console.WriteLine("--- Магазин ---");
        Console.WriteLine($"Назва: {GetName()}");
        Console.WriteLine($"Адреса: {GetAddress()}");
        Console.WriteLine($"Профіль: {GetProfile()}");
        Console.WriteLine($"Телефон: {GetPhone()}");
        Console.WriteLine($"Email: {GetEmail()}");
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public string GetAddress()
    {
        return _address;
    }

    public void SetAddress(string address)
    {
        _address = address;
    }

    public string GetProfile()
    {
        return _profile;
    }

    public void SetProfile(string profile)
    {
        _profile = profile;
    }

    public string GetPhone()
    {
        return _phone;
    }

    public void SetPhone(string phone)
    {
        _phone = phone;
    }

    public string GetEmail()
    {
        return _email;
    }

    public void SetEmail(string email)
    {
        _email = email;
    }
}
