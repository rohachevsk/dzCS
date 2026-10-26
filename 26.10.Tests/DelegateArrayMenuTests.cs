using System.IO;

// Тести за зразком з пари: масив делегатів + вибір пункту меню
// і виклик dg[choice - 1](text).
public class DelegateArrayMenuTests
{
    private static readonly DateTime FixedTime = new DateTime(2026, 10, 26, 14, 5, 9);
    private static string Nl => Environment.NewLine;

    [Fact]
    public void ArrayOfDelegates_IndexSelectsMethod()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);

        // Як у прикладі: MessageHandler[] dg = { Max, Min, Sum, Mult };
        MessageHandler[] dg = { printer.WritePlain, printer.WriteInFrame, printer.WriteWithTime, printer.WriteUpper };

        string[] expected =
        {
            "Привіт" + Nl,
            string.Join(Nl, "**********", "* Привіт *", "**********") + Nl,
            "[26.10.2026 14:05:09] Привіт" + Nl,
            "ПРИВІТ" + Nl
        };

        for (int choice = 1; choice <= 4; choice++)
        {
            output.GetStringBuilder().Clear();

            dg[choice - 1]("Привіт");

            Assert.Equal(expected[choice - 1], output.ToString());
        }
    }

    [Fact]
    public void Menu_Choice2_PrintsFrameResult()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);
        MessageHandler[] dg = { printer.WritePlain, printer.WriteInFrame, printer.WriteWithTime, printer.WriteUpper };
        StringReader input = new StringReader("2" + Nl + "5");

        RunMenu(dg, "abc", input, output);

        string result = output.ToString();

        Assert.Contains(string.Join(Nl, "*******", "* abc *", "*******"), result);
        Assert.DoesNotContain("ABC", result);
        Assert.EndsWith("Ваш вибір: ", result);
    }

    [Fact]
    public void Menu_Choice5_ExitsWithoutCallingAnyDelegate()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);
        MessageHandler[] dg = { printer.WritePlain, printer.WriteInFrame, printer.WriteWithTime, printer.WriteUpper };
        StringReader input = new StringReader("5");

        RunMenu(dg, "abc", input, output);

        string result = output.ToString();

        Assert.DoesNotContain("abc", result);
        Assert.DoesNotContain("ABC", result);
        Assert.Contains("5 Вихід", result);
    }

    [Fact]
    public void Menu_FourChoices_InvokeDelegatesInOrder()
    {
        MessagePrinter printer = CreatePrinter(out StringWriter output);
        MessageHandler[] dg = { printer.WritePlain, printer.WriteInFrame, printer.WriteWithTime, printer.WriteUpper };
        StringReader input = new StringReader("1" + Nl + "2" + Nl + "3" + Nl + "4" + Nl + "5");

        RunMenu(dg, "abc", input, output);

        string result = output.ToString();

        int plain = result.IndexOf("abc" + Nl, StringComparison.Ordinal);
        int frame = result.IndexOf("* abc *", StringComparison.Ordinal);
        int time = result.IndexOf("[26.10.2026 14:05:09] abc", StringComparison.Ordinal);
        int upper = result.IndexOf("ABC" + Nl, StringComparison.Ordinal);

        Assert.True(plain >= 0, "звичайний вивід не знайдено");
        Assert.True(plain < frame, "рамка має бути після звичайного виводу");
        Assert.True(frame < time, "час має бути після рамки");
        Assert.True(time < upper, "великі літери мають бути після часу");
    }

    // Цикл меню як у прикладі: Convert.ToInt32, choice != 5, dg[choice - 1](text).
    private static void RunMenu(MessageHandler[] dg, string text, TextReader input, TextWriter output)
    {
        int choice = 0;

        while (choice != 5)
        {
            output.Write(Nl + "1 Звичайний" + Nl + "2 У рамці" + Nl + "3 З часом" + Nl + "4 Верхній регістр" + Nl + "5 Вихід" + Nl + "Ваш вибір: ");
            choice = Convert.ToInt32(input.ReadLine());

            if (choice >= 1 && choice <= 4)
            {
                dg[choice - 1](text);
            }
        }
    }

    private static MessagePrinter CreatePrinter(out StringWriter output)
    {
        output = new StringWriter();
        return new MessagePrinter(output, () => FixedTime);
    }
}
