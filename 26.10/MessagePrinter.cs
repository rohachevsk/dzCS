using System.Globalization;

// Клас з різними способами виведення повідомлення.
// Пише у TextWriter, тому його легко перевіряти тестами.
public class MessagePrinter
{
    private readonly TextWriter output;
    private readonly Func<DateTime> clock;

    public MessagePrinter(TextWriter output)
        : this(output, () => DateTime.Now)
    {
    }

    public MessagePrinter(TextWriter output, Func<DateTime> clock)
    {
        this.output = output;
        this.clock = clock;
    }

    // Спосіб 1: звичайний вивід.
    public void WritePlain(string text)
    {
        output.WriteLine(text);
    }

    // Спосіб 2: вивід у рамці з зірочок.
    public void WriteInFrame(string text)
    {
        string[] lines = text.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');

        int width = 0;
        foreach (string line in lines)
        {
            if (line.Length > width)
            {
                width = line.Length;
            }
        }

        string border = new string('*', width + 4);

        output.WriteLine(border);
        foreach (string line in lines)
        {
            output.WriteLine("* " + line.PadRight(width) + " *");
        }
        output.WriteLine(border);
    }

    // Спосіб 3: вивід з датою та часом.
    public void WriteWithTime(string text)
    {
        string timestamp = clock().ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        output.WriteLine("[" + timestamp + "] " + text);
    }

    // Спосіб 4: вивід великими літерами.
    public void WriteUpper(string text)
    {
        output.WriteLine(text.ToUpper(CultureInfo.InvariantCulture));
    }
}
