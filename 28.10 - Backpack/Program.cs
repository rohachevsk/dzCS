public static class Program
{
    public static void Main()
    {
        Backpack backpack = new Backpack();
        backpack.SetColor("Чорний");
        backpack.SetBrand("Nike (Vietnam)");
        backpack.SetFabric("Нейлон");
        backpack.SetWeight(1.2);
        backpack.SetVolume(8);

        // Анонімний метод — обробник події додавання предмета.
        backpack.ItemAdded += delegate (Backpack source, BackpackItem item)
        {
            Console.WriteLine($"Додано: {item}. Вільно {source.Volume - source.UsedVolume:0.##} л.");
        };

        backpack.AddItem(new Laptop());
        backpack.AddItem(new Book());
        backpack.AddItem(new WaterBottle());

        try
        {
            backpack.AddItem(new Book("Великий словник", 5, 2));
        }
        catch (InvalidOperationException error)
        {
            Console.WriteLine($"Помилка: {error.Message}");
        }

        Console.WriteLine();
        Console.WriteLine($"Рюкзак: {backpack.Color}, {backpack.Brand}, {backpack.Fabric}");
        Console.WriteLine($"Вага: {backpack.Weight:0.##} кг, об'єм: {backpack.Volume:0.##} л, зайнято: {backpack.UsedVolume:0.##} л");
        Console.WriteLine("Вміст:");

        foreach (BackpackItem item in backpack.Items)
        {
            Console.WriteLine("  " + item);
        }
    }
}
