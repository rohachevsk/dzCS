using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Student olena = new Student("Олена", "Ковальчук", "ІП-21");
        Student taras = new Student("Тарас", "Шевченко", "ІП-22");
        Student maria = new Student("Марія", "Бондаренко", "ІП-21");

        Teacher ivan = new Teacher("Іван", "Петренко", "доцент");
        Teacher oksana = new Teacher("Оксана", "Мельник", "професор");

        Console.WriteLine("=== Агрегація: студенти й викладачі створені незалежно ===");
        olena.DisplayInfo();
        ivan.DisplayInfo();

        University university = new University(
            "Київський національний університет",
            "вул. Володимирська, 60",
            "Київ",
            "01033");

        Course programming = new Course("Програмування", "Основи C#", "ООП", "Колекції");
        Course databases = new Course("Бази даних", "SQL", "Нормалізація", "Транзакції");

        Console.WriteLine();
        Console.WriteLine("=== Композиція: адреса створюється всередині університету ===");
        university.DisplayInfo();
        Console.WriteLine("Адреса через об'єкт університету:");
        university.Address.DisplayInfo();

        Console.WriteLine();
        Console.WriteLine("=== Композиція: теми створюються всередині курсу ===");
        programming.DisplayInfo();

        university.AddTeacher(ivan);
        university.AddTeacher(oksana);

        programming.AddStudent(olena);
        programming.AddStudent(taras);
        databases.AddStudent(taras);
        databases.AddStudent(maria);

        Console.WriteLine();
        university.DisplayTeachers();

        Console.WriteLine();
        programming.DisplayInfo();
        programming.DisplayStudents();

        Console.WriteLine();
        databases.DisplayInfo();
        databases.DisplayStudents();

        Console.WriteLine();
        Console.WriteLine("=== Пошук студента на курсі ===");
        Student? found = programming.FindStudent("Олена");
        Console.WriteLine(found != null
            ? $"За ім'ям знайдено: {found.FullName} (ID {found.Id})"
            : "За ім'ям студента не знайдено.");

        found = programming.FindStudent(taras.Id.ToString());
        Console.WriteLine(found != null
            ? $"За ID знайдено: {found.FullName} (ID {found.Id})"
            : "За ID студента не знайдено.");

        Console.WriteLine();
        Console.WriteLine("=== Незалежність: студент існує після відрахування з курсу ===");
        bool removed = programming.RemoveStudent(olena);
        Console.WriteLine($"Олену видалено з курсу «{programming.Name}»: {removed}");
        olena.DisplayInfo();
        programming.DisplayStudents();

        Console.WriteLine();
        Console.WriteLine("=== Обробка винятків ===");
        TryAction("повторне додавання студента на курс", () => programming.AddStudent(taras));
        TryAction("додавання null-студента", () => programming.AddStudent(null!));
        TryAction("порожнє ім'я студента", () => new Student("   ", "Коваль", "ІП-21"));
        TryAction("некоректний індекс університету", () => new University("Тест", "Вулиця", "Місто", "12"));
        TryAction("порожній пошуковий запит", () => programming.FindStudent("   "));
        TryAction("додавання null-викладача", () => university.AddTeacher(null!));

        bool removedMissing = databases.RemoveStudent(olena);
        Console.WriteLine($"--- видалення студента, якого немає на курсі ---{Environment.NewLine}" +
                          $"Результат: {removedMissing} (винятку немає, метод повертає false)");

        Console.WriteLine();
        Console.WriteLine($"Статичний лічильник: створено курсів — {Course.CreatedCount}");
    }

    static void TryAction(string description, Action action)
    {
        Console.WriteLine($"--- {description} ---");
        try
        {
            action();
            Console.WriteLine("Помилки не було.");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"ArgumentNullException: {ex.Message}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"ArgumentOutOfRangeException: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"ArgumentException: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"InvalidOperationException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
        }
    }
}
