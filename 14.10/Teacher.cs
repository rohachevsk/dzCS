using System;

public class Teacher
{
    private static int _nextId;
    private readonly int _id;

    public Teacher(string firstName, string lastName, string position)
    {
        _id = ++_nextId;
        FirstName = Guard.RequireText(firstName, nameof(firstName), "Ім'я");
        LastName = Guard.RequireText(lastName, nameof(lastName), "Прізвище");
        Position = Guard.RequireText(position, nameof(position), "Посада");
    }

    public int Id
    {
        get
        {
            return _id;
        }
    }

    public string FirstName { get; }

    public string LastName { get; }

    public string Position { get; }

    public string FullName
    {
        get
        {
            return $"{LastName} {FirstName}";
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Викладач #{_id}: {FullName}, посада: {Position}");
    }
}
