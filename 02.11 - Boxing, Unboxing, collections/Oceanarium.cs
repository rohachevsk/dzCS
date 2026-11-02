namespace Primer;

public class SeaItem
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }

    public SeaItem(string name, int age, double weight)
    {
        Name = name;
        Age = age;
        Weight = weight;
    }

    public virtual string GetInfo()
    {
        return $"{Name}: вік {Age} років, вага {Weight} кг";
    }
}

public class Dolphin : SeaItem
{
    public int JumpHeight { get; set; }

    public Dolphin(string name, int age, double weight, int jumpHeight)
        : base(name, age, weight)
    {
        JumpHeight = jumpHeight;
    }

    public override string GetInfo()
    {
        return $"Дельфін {base.GetInfo()}, висота стрибка {JumpHeight} м";
    }
}

public class Shark : SeaItem
{
    public int TeethCount { get; set; }

    public Shark(string name, int age, double weight, int teethCount)
        : base(name, age, weight)
    {
        TeethCount = teethCount;
    }

    public override string GetInfo()
    {
        return $"Акула {base.GetInfo()}, зубів {TeethCount}";
    }
}

public class Octopus : SeaItem
{
    public int ArmsCount { get; set; }

    public Octopus(string name, int age, double weight, int armsCount)
        : base(name, age, weight)
    {
        ArmsCount = armsCount;
    }

    public override string GetInfo()
    {
        return $"Восьминіг {base.GetInfo()}, щупалець {ArmsCount}";
    }
}

public class SeaTurtle : SeaItem
{
    public double ShellDiameter { get; set; }

    public SeaTurtle(string name, int age, double weight, double shellDiameter)
        : base(name, age, weight)
    {
        ShellDiameter = shellDiameter;
    }

    public override string GetInfo()
    {
        return $"Морська черепаха {base.GetInfo()}, панцир {ShellDiameter} см";
    }
}

public class Oceanarium
{
    public string Name { get; set; }

    private readonly List<SeaItem> seaItems = new();

    public Oceanarium(string name)
    {
        Name = name;
    }

    public void Add(SeaItem item)
    {
        seaItems.Add(item);
    }

    public void PrintAll()
    {
        Console.WriteLine($"Жителі океанариуму \"{Name}\":");

        if (seaItems.Count == 0)
        {
            Console.WriteLine("  (порожньо)");
            return;
        }

        foreach (SeaItem item in seaItems)
        {
            Console.WriteLine("  - " + item.GetInfo());
        }
    }
}
