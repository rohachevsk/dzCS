namespace Primer;

using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyNode<T>
{
    public T Data { get; set; }
    public DoublyNode<T>? Next { get; set; }
    public DoublyNode<T>? Previous { get; set; }

    public DoublyNode(T data)
    {
        Data = data;
        Next = null;
        Previous = null;
    }
}

public class DoublyLinkedList<T> : IEnumerable<T>
{
    public DoublyNode<T>? Head { get; private set; }
    public DoublyNode<T>? Tail { get; private set; }
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    public void AddFirst(T data)
    {
        DoublyNode<T> newNode = new DoublyNode<T>(data);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            newNode.Next = Head;
            Head.Previous = newNode;
            Head = newNode;
        }
        Count++;
    }

    public void AddLast(T data)
    {
        DoublyNode<T> newNode = new DoublyNode<T>(data);
        if (Tail == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            newNode.Previous = Tail;
            Tail = newNode;
        }
        Count++;
    }

    public void InsertAt(int index, T data)
    {
        if (index < 0 || index > Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Індекс поза межами списку.");
        }

        if (index == 0)
        {
            AddFirst(data);
            return;
        }

        if (index == Count)
        {
            AddLast(data);
            return;
        }

        DoublyNode<T> current = Head!;
        for (int i = 0; i < index; i++)
        {
            current = current.Next!;
        }

        DoublyNode<T> newNode = new DoublyNode<T>(data);
        newNode.Previous = current.Previous;
        newNode.Next = current;

        current.Previous!.Next = newNode;
        current.Previous = newNode;

        Count++;
    }

    public T RemoveFirst()
    {
        if (Head == null)
        {
            throw new InvalidOperationException("Список порожній.");
        }

        T data = Head.Data;
        Head = Head.Next;
        if (Head != null)
        {
            Head.Previous = null;
        }
        else
        {
            Tail = null;
        }
        Count--;
        return data;
    }

    public T RemoveLast()
    {
        if (Tail == null)
        {
            throw new InvalidOperationException("Список порожній.");
        }

        T data = Tail.Data;
        Tail = Tail.Previous;
        if (Tail != null)
        {
            Tail.Next = null;
        }
        else
        {
            Head = null;
        }
        Count--;
        return data;
    }

    public bool Remove(T data)
    {
        if (Head == null)
        {
            return false;
        }

        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        DoublyNode<T>? current = Head;

        while (current != null)
        {
            if (comparer.Equals(current.Data, data))
            {
                if (current == Head)
                {
                    RemoveFirst();
                }
                else if (current == Tail)
                {
                    RemoveLast();
                }
                else
                {
                    current.Previous!.Next = current.Next;
                    current.Next!.Previous = current.Previous;
                    Count--;
                }
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    public bool Contains(T data)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        DoublyNode<T>? current = Head;
        while (current != null)
        {
            if (comparer.Equals(current.Data, data))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    public void Clear()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }

    public void PrintForward()
    {
        if (IsEmpty)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        Console.Write("Двозв'язний список вперед: ");
        DoublyNode<T>? current = Head;
        while (current != null)
        {
            Console.Write($"{current.Data} <-> ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }

    public void PrintBackward()
    {
        if (IsEmpty)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        Console.Write("Двозв'язний список назад: ");
        DoublyNode<T>? current = Tail;
        while (current != null)
        {
            Console.Write($"{current.Data} <-> ");
            current = current.Previous;
        }
        Console.WriteLine("null");
    }

    public IEnumerator<T> GetEnumerator()
    {
        DoublyNode<T>? current = Head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

