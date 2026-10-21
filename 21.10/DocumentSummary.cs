public record DocumentSummary(string Title, string Author, string DocumentType, int WordCount)
{
    public override string ToString()
    {
        return Title + " — " + Author + " (" + DocumentType + "), слів: " + WordCount;
    }
}
