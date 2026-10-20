using System;
using System.Collections.Generic;

public class Course
{
    private static int _createdCount;
    private readonly List<string> _topics;
    private readonly List<Student> _students;

    public Course(string name, params string[] topics)
    {
        Name = Guard.RequireText(name, nameof(name), "Назва курсу");

        if (topics == null)
        {
            throw new ArgumentNullException(nameof(topics), "Список тем не може бути null.");
        }

        if (topics.Length == 0)
        {
            throw new ArgumentException("Курс повинен містити хоча б одну тему.", nameof(topics));
        }

        _topics = new List<string>();
        foreach (string topic in topics)
        {
            _topics.Add(Guard.RequireText(topic, nameof(topics), "Тема"));
        }

        _students = new List<Student>();
        _createdCount++;
    }

    public string Name { get; }

    public IReadOnlyList<string> Topics
    {
        get
        {
            return _topics;
        }
    }

    public IReadOnlyList<Student> Students
    {
        get
        {
            return _students;
        }
    }

    public static int CreatedCount
    {
        get
        {
            return _createdCount;
        }
    }

    public void AddStudent(Student student)
    {
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Студент не може бути null.");
        }

        if (_students.Contains(student))
        {
            throw new InvalidOperationException($"Студент {student.FullName} вже записаний на курс «{Name}».");
        }

        _students.Add(student);
    }

    public bool RemoveStudent(Student student)
    {
        if (student == null)
        {
            throw new ArgumentNullException(nameof(student), "Студент не може бути null.");
        }

        return _students.Remove(student);
    }

    public Student? FindStudent(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException("Пошуковий запит не може бути порожнім.", nameof(query));
        }

        string text = query.Trim();
        if (int.TryParse(text, out int id))
        {
            foreach (Student student in _students)
            {
                if (student.Id == id)
                {
                    return student;
                }
            }

            return null;
        }

        foreach (Student student in _students)
        {
            if (student.FullName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                student.FirstName.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                student.LastName.Contains(text, StringComparison.OrdinalIgnoreCase))
            {
                return student;
            }
        }

        return null;
    }

    public void DisplayStudents()
    {
        Console.WriteLine($"Студенти курсу «{Name}» ({_students.Count}):");
        if (_students.Count == 0)
        {
            Console.WriteLine("  немає");
            return;
        }

        foreach (Student student in _students)
        {
            Console.WriteLine($"  {student.FullName} (ID {student.Id}), група {student.Group}");
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Курс: {Name}");
        Console.WriteLine($"Теми ({_topics.Count}): {string.Join(", ", _topics)}");
        Console.WriteLine($"Студентів: {_students.Count}");
    }
}
