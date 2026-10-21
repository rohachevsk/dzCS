public interface ITextProcessor
{
    string ProcessText();

    int CountWords();

    bool ContainsWord(string word);
}
