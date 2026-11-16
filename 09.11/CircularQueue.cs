namespace Primer;

using System;
using System.Collections;
using System.Collections.Generic;

public class CircularQueue<T> : IEnumerable<T>
{
    private readonly T[] elements;
    private int head;
    private int tail;
    private int count;

    public CircularQueue(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Місткість черги має бути більшою за нуль.", nameof(capacity));
        }

        Capacity = capacity;
        elements = new T[capacity];
        head = 0;
        tail = 0;
        count = 0;
    }

    public int Capacity { get; }
    public int Count => count;
    public bool IsEmpty => count == 0;
    public bool IsFull => count == Capacity;

    public void Enqueue(T item)
    {
        if (IsFull)
        {
            throw new InvalidOperationException("Кільцева черга переповнена!");
        }

        elements[tail] = item;
        tail = (tail + 1) % Capacity;
        count++;
    }

    public T Dequeue()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Кільцева черга порожня!");
        }

        T item = elements[head];
        elements[head] = default!;
        head = (head + 1) % Capacity;
        count--;
        return item;
    }

    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Кільцева черга порожня!");
        }

        return elements[head];
    }

    public void Clear()
    {
        Array.Clear(elements, 0, Capacity);
        head = 0;
        tail = 0;
        count = 0;
    }

    public void Print()
    {
        if (IsEmpty)
        {
            Console.WriteLine("Кільцева черга порожня.");
            return;
        }

        Console.Write($"Черга [{count}/{Capacity}]: ");
        for (int i = 0; i < count; i++)
        {
            int index = (head + i) % Capacity;
            Console.Write($"{elements[index]} ");
        }
        Console.WriteLine();
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            int index = (head + i) % Capacity;
            yield return elements[index];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

