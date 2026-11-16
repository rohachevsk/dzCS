namespace Primer;

using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string University { get; set; }
    public double AverageGrade { get; set; }

    public Student(string firstName, string lastName, int age, string university, double averageGrade)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        University = university;
        AverageGrade = averageGrade;
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName}, {Age} р. | ВНЗ: {University} | Сер. бал: {AverageGrade:F1}";
    }
}

public static class Task3_StudentQueries
{
    public static void Run()
    {
        Console.WriteLine("--- Завдання 3: LINQ-запити для колекції об'єктів (студентів) ---");

        List<Student> students = new()
        {
            new Student("Олександр", "Петренко", 19, "КНУ", 4.8),
            new Student("Марія", "Коваленко", 21, "КПІ", 4.9),
            new Student("Дмитро", "Мельник", 18, "ЛНУ", 3.7),
            new Student("Олена", "Шевченко", 22, "КНУ", 4.5),
            new Student("Іван", "Бойко", 20, "КПІ", 4.2),
            new Student("Анна", "Ткаченко", 19, "ХНУ", 4.6),
            new Student("Василь", "Кравченко", 23, "ЛНУ", 3.9)
        };

        Console.WriteLine("1. Список усіх студентів:");
        var all = from s in students
                  select s;
        foreach (var s in all) Console.WriteLine("  " + s);

        Console.WriteLine("\n2. Студенти старше 19 років:");
        var older = from s in students
                    where s.Age > 19
                    orderby s.Age ascending
                    select s;
        foreach (var s in older) Console.WriteLine("  " + s);

        Console.WriteLine("\n3. Студенти університету 'КПІ':");
        var kpi = from s in students
                  where s.University == "КПІ"
                  select s;
        foreach (var s in kpi) Console.WriteLine("  " + s);

        Console.WriteLine("\n4. Відмінники (середній бал >= 4.5), відсортовані за спаданням балу:");
        var top = from s in students
                  where s.AverageGrade >= 4.5
                  orderby s.AverageGrade descending
                  select s;
        foreach (var s in top) Console.WriteLine("  " + s);

        Console.WriteLine("\n5. Студенти, відсортовані за прізвищем:");
        var sortedByName = from s in students
                           orderby s.LastName ascending, s.FirstName ascending
                           select s;
        foreach (var s in sortedByName) Console.WriteLine("  " + s);
    }
}

