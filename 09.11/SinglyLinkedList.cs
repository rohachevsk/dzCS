namespace Primer;

using System;
using System.Collections;
using System.Collections.Generic;

public class SinglyNode<T>
{
    public T Data { get; set; }
    public SinglyNode<T>? Next { get; set; }

    public SinglyNode(T data)
    {
        Data = data;
        Next = null;
    }
}

public class SinglyLinkedList<T> : IEnumerable<T>
{
    public SinglyNode<T>? Head { get; private set; }
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    public void AddFirst(T data)
    {
        SinglyNode<T> newNode = new SinglyNode<T>(data);
        newNode.Next = Head;
        Head = newNode;
        Count++;
    }

    public void AddLast(T data)
    {
        SinglyNode<T> newNode = new SinglyNode<T>(data);
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            SinglyNode<T> current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
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

        SinglyNode<T> current = Head!;
        for (int i = 0; i < index - 1; i++)
        {
            current = current.Next!;
        }

        SinglyNode<T> newNode = new SinglyNode<T>(data);
        newNode.Next = current.Next;
        current.Next = newNode;
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
        Count--;
        return data;
    }

    public T RemoveLast()
    {
        if (Head == null)
        {
            throw new InvalidOperationException("Список порожній.");
        }

        if (Head.Next == null)
        {
            T data = Head.Data;
            Head = null;
            Count--;
            return data;
        }

        SinglyNode<T> current = Head;
        while (current.Next!.Next != null)
        {
            current = current.Next;
        }

        T removedData = current.Next.Data;
        current.Next = null;
        Count--;
        return removedData;
    }

    public bool Remove(T data)
    {
        if (Head == null)
        {
            return false;
        }

        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        if (comparer.Equals(Head.Data, data))
        {
            Head = Head.Next;
            Count--;
            return true;
        }

        SinglyNode<T> current = Head;
        while (current.Next != null && !comparer.Equals(current.Next.Data, data))
        {
            current = current.Next;
        }

        if (current.Next != null)
        {
            current.Next = current.Next.Next;
            Count--;
            return true;
        }

        return false;
    }

    public bool Contains(T data)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        SinglyNode<T>? current = Head;
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
        Count = 0;
    }

    public void Print()
    {
        if (IsEmpty)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        Console.Write($"Однозв'язний список (елементів: {Count}): ");
        SinglyNode<T>? current = Head;
        while (current != null)
        {
            Console.Write($"{current.Data} -> ");
            current = current.Next;
        }
        Console.WriteLine("null");
    }

    public IEnumerator<T> GetEnumerator()
    {
        SinglyNode<T>? current = Head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

