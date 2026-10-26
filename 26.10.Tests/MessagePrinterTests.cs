using System.Text.RegularExpressions;

public class MessagePrinterTests
{
    private static readonly DateTime FixedTime = new DateTime(2026, 10, 26, 14, 5, 9);
    private static string Nl => Environment.NewLine;

    [Fact]
    public void WritePlain_WritesTextAndNewLine()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);

        printer.WritePlain("Привіт, світе!");

        Assert.Equal("Привіт, світе!" + Nl, output.ToString());
    }

    [Fact]
    public void WritePlain_EmptyText_WritesEmptyLine()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);

        printer.WritePlain("");

        Assert.Equal(Nl, output.ToString());
    }

    [Fact]
    public void WriteInFrame_SingleLine_WrapsTextInStars()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);

        printer.WriteInFrame("Привіт");

        string expected = string.Join(Nl, "**********", "* Привіт *", "**********") + Nl;
        Assert.Equal(expected, output.ToString());
    }

    [Fact]
    public void WriteInFrame_Multiline_UsesLongestLineAsWidth()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);

        printer.WriteInFrame("abc" + Nl + "defgh");

        string expected = string.Join(Nl, "*********", "* abc   *", "* defgh *", "*********") + Nl;
        Assert.Equal(expected, output.ToString());
    }

    [Fact]
    public void WriteWithTime_PrefixesTimestampFromClock()
    {
        MessagePrinter printer = CreatePrinterWithClock(out StringWriter output);

        printer.WriteWithTime("Текст");

        Assert.Equal("[26.10.2026 14:05:09] Текст" + Nl, output.ToString());
    }

    [Fact]
    public void WriteWithTime_DoesNotChangeText()
    {
        MessagePrinter printer = CreatePrinterWithClock(out StringWriter output);

        printer.WriteWithTime("місто");

        Assert.Equal("[26.10.2026 14:05:09] місто" + Nl, output.ToString());
    }

    [Fact]
    public void WriteWithTime_ByDefault_UsesCurrentTime()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);

        printer.WriteWithTime("Текст");

        Assert.Matches(new Regex(@"^\[\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}:\d{2}\] Текст" + Regex.Escape(Nl) + "$"),
            output.ToString());
    }

    [Fact]
    public void WriteUpper_UppercasesUkrainianLetters()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);

        printer.WriteUpper("привіт, їжак, єнот, і");

        Assert.Equal("ПРИВІТ, ЇЖАК, ЄНОТ, І" + Nl, output.ToString());
    }

    [Fact]
    public void AllMethods_CanBeCalledThroughDelegateArray()
    {
        MessagePrinter printer = CreatePrinterWithClock(out StringWriter output);
        MessageHandler[] handlers =
        {
            printer.WritePlain,
            printer.WriteInFrame,
            printer.WriteWithTime,
            printer.WriteUpper
        };

        foreach (MessageHandler handler in handlers)
        {
            handler("abc");
        }

        string expected = string.Join(Nl,
            "abc",
            "*******",
            "* abc *",
            "*******",
            "[26.10.2026 14:05:09] abc",
            "ABC") + Nl;

        Assert.Equal(expected, output.ToString());
    }

    private static MessagePrinter CreatePrinter(out StringWriter output)
    {
        output = new StringWriter();
        return new MessagePrinter(output);
    }

    private static MessagePrinter CreatePrinterWithClock(out StringWriter output)
    {
        output = new StringWriter();
        return new MessagePrinter(output, () => FixedTime);
    }
}
