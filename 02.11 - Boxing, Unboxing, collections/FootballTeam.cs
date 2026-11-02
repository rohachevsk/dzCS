namespace Primer;

public class Player
{
    public string Name { get; set; }
    public int Number { get; set; }
    public string Position { get; set; }

    public Player(string name, int number, string position)
    {
        Name = name;
        Number = number;
        Position = position;
    }

    public string GetInfo()
    {
        return $"#{Number} {Name} ({Position})";
    }
}

public class FootballTeam
{
    public string Name { get; set; }

    private readonly List<Player> players = new();

    public FootballTeam(string name)
    {
        Name = name;
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    public void PrintAll()
    {
        Console.WriteLine($"Склад команди \"{Name}\":");

        if (players.Count == 0)
        {
            Console.WriteLine("  (порожньо)");
            return;
        }

        foreach (Player player in players)
        {
            Console.WriteLine("  - " + player.GetInfo());
        }
    }
}
