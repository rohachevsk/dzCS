namespace Primer;

using System.Collections;
using System.Collections.Generic;

public class Alphabet : IEnumerable<char>
{
    private readonly char[] letters;

    public Alphabet()
    {
        letters = new char[26];
        for (int i = 0; i < 26; i++)
        {
            letters[i] = (char)('A' + i);
        }
    }

    public int Length => letters.Length;

    public char this[int index] => letters[index];

    public IEnumerator<char> GetEnumerator()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            yield return letters[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

