using System;

public class Student
{
    private static int _nextId;
    private readonly int _id;

    public Student(string firstName, string lastName, string group)
    {
        _id = ++_nextId;
        FirstName = Guard.RequireText(firstName, nameof(firstName), "Ім'я");
        LastName = Guard.RequireText(lastName, nameof(lastName), "Прізвище");
        Group = Guard.RequireText(group, nameof(group), "Група");
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

    public string Group { get; }

    public string FullName
    {
        get
        {
            return $"{LastName} {FirstName}";
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Студент #{_id}: {FullName}, група {Group}");
    }
}
