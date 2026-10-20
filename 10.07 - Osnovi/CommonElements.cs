using System;
using System.Collections.Generic;

public static class CommonElements
{
    public static int[] GetCommon(int[] first, int[] second)
    {
        if (first == null)
        {
            throw new ArgumentNullException(nameof(first), "Перший масив не може бути null.");
        }

        if (second == null)
        {
            throw new ArgumentNullException(nameof(second), "Другий масив не може бути null.");
        }

        List<int> result = new List<int>();
        for (int i = 0; i < first.Length; i++)
        {
            bool foundInSecond = false;
            for (int j = 0; j < second.Length; j++)
            {
                if (first[i] == second[j])
                {
                    foundInSecond = true;
                    break;
                }
            }

            if (foundInSecond && !result.Contains(first[i]))
            {
                result.Add(first[i]);
            }
        }

        return result.ToArray();
    }
}
