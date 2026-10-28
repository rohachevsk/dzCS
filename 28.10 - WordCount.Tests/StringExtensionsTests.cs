public class StringExtensionsTests
{
    [Fact]
    public void CountWords_Null_ReturnsZero()
    {
        string? text = null;

        Assert.Equal(0, text.CountWords());
    }

    [Fact]
    public void CountWords_Empty_ReturnsZero()
    {
        Assert.Equal(0, "".CountWords());
    }

    [Fact]
    public void CountWords_WhitespaceOnly_ReturnsZero()
    {
        Assert.Equal(0, " \t\n\r ".CountWords());
    }

    [Fact]
    public void CountWords_SingleWord_ReturnsOne()
    {
        Assert.Equal(1, "Привіт".CountWords());
    }

    [Fact]
    public void CountWords_Sentence_CountsWords()
    {
        Assert.Equal(2, "Привіт світ".CountWords());
    }

    [Fact]
    public void CountWords_ExtraSpaces_Ignored()
    {
        Assert.Equal(2, "  a   b  ".CountWords());
    }

    [Fact]
    public void CountWords_MixedWhitespace_CountsWords()
    {
        Assert.Equal(3, "one\ttwo\nthree".CountWords());
    }

    [Fact]
    public void CountWords_Punctuation_KeepsWordCount()
    {
        Assert.Equal(2, "Привіт, світ!".CountWords());
    }

    [Fact]
    public void CountWords_HyphenatedWord_ReturnsOne()
    {
        Assert.Equal(1, "hello-world".CountWords());
    }

    [Fact]
    public void CountWords_UkrainianText_CountsWords()
    {
        Assert.Equal(3, "Я люблю програмувати".CountWords());
    }

    [Fact]
    public void CountWords_TrailingWhitespace_IgnoresIt()
    {
        Assert.Equal(3, "один два три   \n".CountWords());
    }
}
