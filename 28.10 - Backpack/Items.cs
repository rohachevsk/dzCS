public class Book : BackpackItem
{
    public Book(string name = "Книга", double volume = 1.5, double weight = 0.8)
        : base(name, volume, weight)
    {
    }
}

public class Laptop : BackpackItem
{
    public Laptop(string name = "Ноутбук", double volume = 4.0, double weight = 2.2)
        : base(name, volume, weight)
    {
    }
}

public class WaterBottle : BackpackItem
{
    public WaterBottle(string name = "Пляшка води", double volume = 0.75, double weight = 0.6)
        : base(name, volume, weight)
    {
    }
}

public class Notebook : BackpackItem
{
    public Notebook(string name = "Зошит", double volume = 0.5, double weight = 0.3)
        : base(name, volume, weight)
    {
    }
}
