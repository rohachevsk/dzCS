using System;
using System.Collections.Generic;

public class University
{
    private readonly Address _address;
    private readonly List<Teacher> _teachers;

    public University(string name, string street, string city, string zipCode)
    {
        Name = Guard.RequireText(name, nameof(name), "Назва університету");
        _address = new Address(street, city, zipCode);
        _teachers = new List<Teacher>();
    }

    public string Name { get; }

    public Address Address
    {
        get
        {
            return _address;
        }
    }

    public IReadOnlyList<Teacher> Teachers
    {
        get
        {
            return _teachers;
        }
    }

    public void AddTeacher(Teacher teacher)
    {
        if (teacher == null)
        {
            throw new ArgumentNullException(nameof(teacher), "Викладач не може бути null.");
        }

        if (_teachers.Contains(teacher))
        {
            throw new InvalidOperationException($"Викладач {teacher.FullName} вже працює в університеті «{Name}».");
        }

        _teachers.Add(teacher);
    }

    public void DisplayTeachers()
    {
        Console.WriteLine($"Викладачі університету «{Name}» ({_teachers.Count}):");
        if (_teachers.Count == 0)
        {
            Console.WriteLine("  немає");
            return;
        }

        foreach (Teacher teacher in _teachers)
        {
            Console.WriteLine($"  {teacher.FullName} — {teacher.Position} (ID {teacher.Id})");
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Університет: {Name}");
        _address.DisplayInfo();
        Console.WriteLine($"Викладачів: {_teachers.Count}");
    }
}
