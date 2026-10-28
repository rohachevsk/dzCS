public delegate void ItemAddedHandler(Backpack backpack, BackpackItem item);

public class Backpack
{
    public string Color { get; private set; } = "";
    public string Brand { get; private set; } = "";
    public string Fabric { get; private set; } = "";
    public double Weight { get; private set; }
    public double Volume { get; private set; }

    public List<BackpackItem> Items { get; } = new List<BackpackItem>();

    public double UsedVolume => Items.Sum(item => item.Volume);

    public event ItemAddedHandler? ItemAdded;

    public void SetColor(string color) => Color = color;

    public void SetBrand(string brand) => Brand = brand;

    public void SetFabric(string fabric) => Fabric = fabric;

    public void SetWeight(double weight) => Weight = weight;

    public void SetVolume(double volume) => Volume = volume;

    public void AddItem(BackpackItem item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        if (UsedVolume + item.Volume > Volume)
        {
            throw new InvalidOperationException(
                $"Недостатньо місця: потрібно {item.Volume:0.##} л, вільно {Volume - UsedVolume:0.##} л.");
        }

        Items.Add(item);
        ItemAdded?.Invoke(this, item);
    }
}
