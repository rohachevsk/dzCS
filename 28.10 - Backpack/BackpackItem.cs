public class BackpackItem
{
    public string Name { get; }
    public double Volume { get; }
    public double Weight { get; }

    public BackpackItem(string name, double volume, double weight)
    {
        Name = name;
        Volume = volume;
        Weight = weight;
    }

    public override string ToString()
    {
        return $"{Name} ({Volume:0.##} л, {Weight:0.##} кг)";
    }
}
