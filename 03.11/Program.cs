namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 03.11 Ітератори та foreach ===");
            Console.WriteLine("1 — Завдання 1: Клас «Алфавіт»");
            Console.WriteLine("2 — Завдання 2: Класи «Будинок» і «Квартира»");
            Console.WriteLine("3 — Завдання 3: Клас «Гараж» та «Автомобіль»");
            Console.WriteLine("4 — Запустити всі три завдання");
            Console.WriteLine("0 — Вихід");
            Console.Write("Виберіть пункт: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    RunTask1();
                    break;
                case "2":
                    RunTask2();
                    break;
                case "3":
                    RunTask3();
                    break;
                case "4":
                    RunTask1();
                    Console.WriteLine(new string('-', 50));
                    RunTask2();
                    Console.WriteLine(new string('-', 50));
                    RunTask3();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void RunTask1()
    {
        Console.WriteLine("--- Завдання 1: Алфавіт (foreach) ---");
        Alphabet alphabet = new Alphabet();

        Console.Write("Літери алфавіту через foreach: ");
        foreach (char letter in alphabet)
        {
            Console.Write(letter + " ");
        }
        Console.WriteLine();
    }

    private static void RunTask2()
    {
        Console.WriteLine("--- Завдання 2: Будинок і Квартири (foreach) ---");
        House house = new House("вул. Хрещатик, 24");

        Apartment apt1 = new Apartment(101, 2);
        apt1.AddResident("Шевченко Тарас", 35);
        apt1.AddResident("Шевченко Оксана", 32);

        Apartment apt2 = new Apartment(102, 3);
        apt2.AddResident("Франко Іван", 40);
        apt2.AddResident("Франко Ольга", 38);
        apt2.AddResident("Франко Петро", 14);

        Apartment apt3 = new Apartment(103, 1);
        apt3.AddResident("Коваленко Андрій", 24);

        house.AddApartment(apt1);
        house.AddApartment(apt2);
        house.AddApartment(apt3);

        Console.WriteLine(house);
        Console.WriteLine();

        Console.WriteLine("Тестування foreach для класу «Будинок» та «Квартира»:");
        foreach (Apartment apartment in house)
        {
            Console.WriteLine($"-> {apartment}");
            foreach (Resident resident in apartment)
            {
                Console.WriteLine($"     - {resident}");
            }
        }
    }

    private static void RunTask3()
    {
        Console.WriteLine("--- Завдання 3: Гараж і Автомобілі (foreach) ---");
        Garage garage = new Garage("Центральний", "вул. Перемоги, 15");

        garage.AddCar(new Car("Toyota", "Camry", 2020, "AA1234BB", "Чорний"));
        garage.AddCar(new Car("BMW", "X5", 2022, "BC5678CT", "Синій"));
        garage.AddCar(new Car("Audi", "A6", 2019, "AE9012KA", "Сріблястий"));
        garage.AddCar(new Car("Volkswagen", "Golf", 2018, "AM3456BH", "Червоний"));

        Console.WriteLine(garage);
        Console.WriteLine();

        Console.WriteLine("Тестування foreach для класу «Гараж»:");
        int i = 1;
        foreach (Car car in garage)
        {
            Console.WriteLine($"  {i++}. {car}");
        }
    }
}

