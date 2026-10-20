using System;

public class Magazine
{
    private string _name;
    private int _year;
    private string _description;
    private string _phone;
    private string _email;

    public Magazine()
    {
        _name = "default";
        _year = 2000;
        _description = "default";
        _phone = "default";
        _email = "default";
    }

    public void Input()
    {
        Console.Write("Назва журналу: ");
        SetName(Console.ReadLine() ?? string.Empty);

        Console.Write("Рік заснування: ");
        int.TryParse(Console.ReadLine(), out int year);
        SetYear(year);

        Console.Write("Опис журналу: ");
        SetDescription(Console.ReadLine() ?? string.Empty);

        Console.Write("Контактний телефон: ");
        SetPhone(Console.ReadLine() ?? string.Empty);

        Console.Write("Email: ");
        SetEmail(Console.ReadLine() ?? string.Empty);
    }

    public void Print()
    {
        Console.WriteLine("--- Журнал ---");
        Console.WriteLine($"Назва: {GetName()}");
        Console.WriteLine($"Рік заснування: {GetYear()}");
        Console.WriteLine($"Опис: {GetDescription()}");
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

    public int GetYear()
    {
        return _year;
    }

    public void SetYear(int year)
    {
        _year = year;
    }

    public string GetDescription()
    {
        return _description;
    }

    public void SetDescription(string description)
    {
        _description = description;
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
