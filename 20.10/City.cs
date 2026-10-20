internal class City
{
    public string Name { get; set; }
    public int[] Data { get; set; } = new int[3]; // 0 - population, 1 - level_pollution(рівень забруднення), 2 - area
    public City()
    {
        Name = "default";
        Data[0] = 50000;
        Data[1] = 6;
        Data[2] = 117236;
    }
public City(string name, params int[] data)
{
    Name = name;
    Data[0] = data[0];
    Data[1] = data[1];
    Data[2] = data[2];
}
public override string ToString()
{
    return $"City: {Name} Data: {Data[0]}, {Data[1]} , {Data[2]}";
}
    }