namespace Primer;

using System;
using System.Collections.Generic;

public class BrowserHistory
{
    private readonly LinkedList<string> history = new();
    private LinkedListNode<string>? current;

    public BrowserHistory(string homepage)
    {
        current = history.AddFirst(homepage);
    }

    public void Visits(string url)
    {
        if (current == null)
        {
            current = history.AddFirst(url);
            return;
        }

        while (current.Next != null)
        {
            history.Remove(current.Next);
        }

        current = history.AddAfter(current, url);
    }

    public string Back(int steps)
    {
        while (steps > 0 && current?.Previous != null)
        {
            current = current.Previous;
            steps--;
        }

        return GetCurrentPage();
    }

    public string Forward(int steps)
    {
        while (steps > 0 && current?.Next != null)
        {
            current = current.Next;
            steps--;
        }

        return GetCurrentPage();
    }

    public string GetCurrentPage()
    {
        return current?.Value ?? "Історія порожня";
    }

    public void ShowHistory()
    {
        if (history.Count == 0)
        {
            Console.WriteLine("Історія порожня");
            return;
        }

        List<string> parts = new();
        for (LinkedListNode<string>? node = history.First; node != null; node = node.Next)
        {
            if (node == current)
            {
                parts.Add($"[ {node.Value} ]");
            }
            else
            {
                parts.Add(node.Value);
            }
        }

        Console.WriteLine("Історія: " + string.Join(" -> ", parts));
    }
}
