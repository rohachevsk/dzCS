using System;

namespace Delegate
{
    public delegate bool Comparer(object elem1, object elem2);

    static class SelectionSorter
    {
        static public void Sort(object[] array, Comparer comparer)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int best = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (comparer(array[j], array[best]))
                    {
                        best = j;
                    }
                }
                object buffer = array[i];
                array[i] = array[best];
                array[best] = buffer;
            }
        }
    }

    public struct Person
    {
        public string FirstName;
        public string LastName;
        public DateTime Birthday;

        public Person(string firstName, string lastName, DateTime birthday)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Birthday = birthday;
        }

        public override string ToString()
        {
            return string.Format(
                "Имя: {0,-10} Фамилия: {1,-10} Дата рождения: {2:d}.",
                FirstName, LastName, Birthday);
        }
    }

    public class Product
    {
        public string Title;
        public decimal Price;
        public int Count;

        public Product(string title, decimal price, int count)
        {
            Title = title;
            Price = price;
            Count = count;
        }

        public override string ToString()
        {
            return string.Format("Товар: {0,-20} Цена: {1,6:C} Кол-во: {2,3}", Title, Price, Count);
        }
    }

    class Program
    {
        static public bool PersonBirthdayComparer(object person1, object person2)
        {
            return ((Person)person1).Birthday > ((Person)person2).Birthday;
        }

        static public bool ProductTitleComparer(object p1, object p2)
        {
            return string.Compare(((Product)p1).Title, ((Product)p2).Title) < 0;
        }

        static public bool ProductPriceComparer(object p1, object p2)
        {
            return ((Product)p1).Price < ((Product)p2).Price;
        }

        static public bool ProductCountComparer(object p1, object p2)
        {
            return ((Product)p1).Count < ((Product)p2).Count;
        }

        static void Main(string[] args)
        {
            Person p0 = new Person("Максим", "Орлов", new DateTime(1989, 5, 12));
            Person p1 = new Person("Иван", "Иванов", new DateTime(1985, 7, 21));
            Person p2 = new Person("Петр", "Петров", new DateTime(1991, 3, 1));
            Person p3 = new Person("Федор", "Федоров", new DateTime(1971, 11, 25));
            Person p4 = new Person("Павел", "Козлов", new DateTime(1966, 8, 6));

            object[] persons = { p0, p1, p2, p3, p4 };

            Console.WriteLine("\nНеупорядоченный список:");
            foreach (object person in persons) Console.WriteLine(person);

            Console.WriteLine("\nОтсортированный список:");
            SelectionSorter.Sort(persons, PersonBirthdayComparer);
            foreach (object person in persons) 
                Console.WriteLine(person);

            Console.WriteLine("\n");

            Product prod0 = new Product("Молоко", 50m, 10);
            Product prod1 = new Product("Хлеб", 30m, 5);
            Product prod2 = new Product("Яблоко", 40m, 20);
            Product prod3 = new Product("Банан", 60m, 15);
            Product prod4 = new Product("Апельсин", 45m, 8);

            object[] products = { prod0, prod1, prod2, prod3, prod4 };

            Console.WriteLine("\nНеупорядоченный список товаров:");
            foreach (object p in products) Console.WriteLine(p);

            Console.WriteLine("\nСортировка по Title:");
            SelectionSorter.Sort(products, ProductTitleComparer);
            foreach (object p in products) Console.WriteLine(p);

            object[] productsByPrice = { prod0, prod1, prod2, prod3, prod4 };
            Console.WriteLine("\nСортировка по Price:");
            SelectionSorter.Sort(productsByPrice, ProductPriceComparer);
            foreach (object p in productsByPrice) Console.WriteLine(p);

            object[] productsByCount = { prod0, prod1, prod2, prod3, prod4 };
            Console.WriteLine("\nСортировка по Count:");
            SelectionSorter.Sort(productsByCount, ProductCountComparer);
            foreach (object p in productsByCount) Console.WriteLine(p);

            Console.WriteLine("\n");
        }
    }
}
