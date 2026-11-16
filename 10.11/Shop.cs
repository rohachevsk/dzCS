namespace Primer;

using System;

public enum ShopType
{
    Grocery,
    Hardware,
    Clothes,
    Shoes
}

public class Shop : IDisposable
{
    private bool disposed = false;

    public string Name { get; set; }
    public string Address { get; set; }
    public ShopType Type { get; set; }

    public Shop(string name, string address, ShopType type)
    {
        Name = name;
        Address = address;
        Type = type;
        Console.WriteLine($"[Конструктор] Магазин \"{Name}\" створено.");
    }

    public string GetTypeDescription()
    {
        return Type switch
        {
            ShopType.Grocery => "Продовольчий",
            ShopType.Hardware => "Господарський",
            ShopType.Clothes => "Одяг",
            ShopType.Shoes => "Взуття",
            _ => "Невідомий"
        };
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Магазин: \"{Name}\" | Адреса: {Address} | Тип: {GetTypeDescription()}");
    }

    public override string ToString()
    {
        return $"\"{Name}\" ({GetTypeDescription()}, {Address})";
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Console.WriteLine($"[Dispose] Звільнення керованих ресурсів магазину \"{Name}\".");
            }
            Console.WriteLine($"[Dispose/Finalize] Закриття ресурсів магазину \"{Name}\".");
            disposed = true;
        }
    }

    ~Shop()
    {
        Console.WriteLine($"[Деструктор ~Shop()] Викликано деструктор для магазину \"{Name}\".");
        Dispose(false);
    }
}
