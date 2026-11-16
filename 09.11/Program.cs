namespace Primer;

internal static class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=== 09.11 Структури даних ===");
            Console.WriteLine("1 — Завдання 3: Кільцева черга (CircularQueue<T>)");
            Console.WriteLine("2 — Завдання 4: Однозв'язний список (SinglyLinkedList<T>)");
            Console.WriteLine("3 — Завдання 5: Двозв'язний список (DoublyLinkedList<T>)");
            Console.WriteLine("4 — Запустити демонстрацію всіх трьох завдань");
            Console.WriteLine("0 — Вихід");
            Console.Write("Виберіть пункт: ");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    DemoCircularQueue();
                    break;
                case "2":
                    DemoSinglyLinkedList();
                    break;
                case "3":
                    DemoDoublyLinkedList();
                    break;
                case "4":
                    DemoCircularQueue();
                    Console.WriteLine(new string('-', 60));
                    DemoSinglyLinkedList();
                    Console.WriteLine(new string('-', 60));
                    DemoDoublyLinkedList();
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

    private static void DemoCircularQueue()
    {
        Console.WriteLine("--- Завдання 3: Кільцева черга (CircularQueue<int>) ---");
        CircularQueue<int> queue = new CircularQueue<int>(5);

        Console.WriteLine("Створено чергу з Capacity = 5");
        Console.WriteLine($"IsEmpty = {queue.IsEmpty}, Count = {queue.Count}");

        Console.WriteLine("Додаємо 10, 20, 30, 40:");
        queue.Enqueue(10);
        queue.Enqueue(20);
        queue.Enqueue(30);
        queue.Enqueue(40);
        queue.Print();

        Console.WriteLine($"Peek(): {queue.Peek()}");
        Console.WriteLine($"Dequeue(): {queue.Dequeue()}");
        Console.WriteLine($"Dequeue(): {queue.Dequeue()}");
        queue.Print();

        Console.WriteLine("Додаємо 50, 60, 70 (перехід через край буфера):");
        queue.Enqueue(50);
        queue.Enqueue(60);
        queue.Enqueue(70);
        queue.Print();
        Console.WriteLine($"IsFull: {queue.IsFull}");

        Console.Write("Обхід черги через foreach: ");
        foreach (int item in queue)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
    }

    private static void DemoSinglyLinkedList()
    {
        Console.WriteLine("--- Завдання 4: Однозв'язний список (SinglyLinkedList<string>) ---");
        SinglyLinkedList<string> list = new SinglyLinkedList<string>();

        Console.WriteLine("Додаємо в кінець: 'Київ', 'Львів', 'Одеса':");
        list.AddLast("Київ");
        list.AddLast("Львів");
        list.AddLast("Одеса");
        list.Print();

        Console.WriteLine("Додаємо на початок: 'Харків':");
        list.AddFirst("Харків");
        list.Print();

        Console.WriteLine("Вставляємо за індексом 2: 'Дніпро':");
        list.InsertAt(2, "Дніпро");
        list.Print();

        Console.WriteLine($"Contains('Львів'): {list.Contains("Львів")}");
        Console.WriteLine($"Contains('Полтава'): {list.Contains("Полтава")}");

        Console.WriteLine($"Видаляємо перший елемент: '{list.RemoveFirst()}'");
        list.Print();

        Console.WriteLine($"Видаляємо останній елемент: '{list.RemoveLast()}'");
        list.Print();

        Console.WriteLine("Видаляємо 'Дніпро': " + list.Remove("Дніпро"));
        list.Print();

        Console.Write("Обхід списку через foreach: ");
        foreach (string city in list)
        {
            Console.Write($"[{city}] ");
        }
        Console.WriteLine();
    }

    private static void DemoDoublyLinkedList()
    {
        Console.WriteLine("--- Завдання 5: Двозв'язний список (DoublyLinkedList<int>) ---");
        DoublyLinkedList<int> list = new DoublyLinkedList<int>();

        Console.WriteLine("Додаємо в кінець: 10, 20, 30:");
        list.AddLast(10);
        list.AddLast(20);
        list.AddLast(30);
        list.PrintForward();

        Console.WriteLine("Додаємо на початок: 5:");
        list.AddFirst(5);
        list.PrintForward();

        Console.WriteLine("Вставляємо за індексом 2 число 15:");
        list.InsertAt(2, 15);
        list.PrintForward();

        Console.WriteLine("Вивід списку у зворотному напрямку (Tail -> Head):");
        list.PrintBackward();

        Console.WriteLine($"Видаляємо 15: {list.Remove(15)}");
        list.PrintForward();

        Console.WriteLine($"RemoveFirst(): {list.RemoveFirst()}");
        Console.WriteLine($"RemoveLast(): {list.RemoveLast()}");
        list.PrintForward();

        Console.Write("Обхід списку через foreach: ");
        foreach (int val in list)
        {
            Console.Write($"{val} ");
        }
        Console.WriteLine();
    }
}

