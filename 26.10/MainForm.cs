using System.IO;

// Головне вікно: вводимо повідомлення і показуємо його різними способами
// через делегат MessageHandler.
public class MainForm : Form
{
    private readonly TextBox messageBox = new TextBox();
    private readonly TextBox outputBox = new TextBox();

    public MainForm()
    {
        Text = "Делегати: виведення повідомлення";
        ClientSize = new Size(760, 520);
        MinimumSize = new Size(620, 420);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10F);

        Label messageLabel = new Label
        {
            Text = "Повідомлення:",
            Location = new Point(12, 12),
            AutoSize = true
        };

        messageBox.Location = new Point(12, 36);
        messageBox.Width = ClientSize.Width - 24;
        messageBox.Text = "Привіт, світе!";
        messageBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        FlowLayoutPanel buttonPanel = new FlowLayoutPanel
        {
            Location = new Point(12, 74),
            Size = new Size(ClientSize.Width - 24, 80),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };

        buttonPanel.Controls.Add(CreateButton("Звичайний", PlainButton_Click));
        buttonPanel.Controls.Add(CreateButton("У рамці", FrameButton_Click));
        buttonPanel.Controls.Add(CreateButton("З часом", TimeButton_Click));
        buttonPanel.Controls.Add(CreateButton("ВЕРХНІЙ регістр", UpperButton_Click));
        buttonPanel.Controls.Add(CreateButton("Показати все", AllButton_Click));
        buttonPanel.Controls.Add(CreateButton("Очистити", ClearButton_Click));

        Label outputLabel = new Label
        {
            Text = "Результат:",
            Location = new Point(12, 160),
            AutoSize = true
        };

        outputBox.Location = new Point(12, 184);
        outputBox.Multiline = true;
        outputBox.ReadOnly = true;
        outputBox.ScrollBars = ScrollBars.Vertical;
        outputBox.BackColor = Color.White;
        outputBox.Font = new Font("Consolas", 11F);
        outputBox.Size = new Size(ClientSize.Width - 24, ClientSize.Height - 196);
        outputBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        Controls.Add(messageLabel);
        Controls.Add(messageBox);
        Controls.Add(buttonPanel);
        Controls.Add(outputLabel);
        Controls.Add(outputBox);
    }

    private static Button CreateButton(string text, EventHandler handler)
    {
        Button button = new Button
        {
            Text = text,
            AutoSize = true,
            MinimumSize = new Size(120, 34),
            Margin = new Padding(0, 0, 6, 6)
        };

        button.Click += handler;
        return button;
    }

    // Одна кнопка - один делегат на один метод.
    private void PlainButton_Click(object? sender, EventArgs e)
    {
        ShowOne("Звичайний вивід", printer => printer.WritePlain);
    }

    private void FrameButton_Click(object? sender, EventArgs e)
    {
        ShowOne("Вивід у рамці", printer => printer.WriteInFrame);
    }

    private void TimeButton_Click(object? sender, EventArgs e)
    {
        ShowOne("Вивід з датою та часом", printer => printer.WriteWithTime);
    }

    private void UpperButton_Click(object? sender, EventArgs e)
    {
        ShowOne("Вивід великими літерами", printer => printer.WriteUpper);
    }

    // Кнопка "Показати все" - працює з масивом делегатів.
    private void AllButton_Click(object? sender, EventArgs e)
    {
        if (!TryGetMessage(out string text))
        {
            return;
        }

        StringWriter writer = new StringWriter();
        MessagePrinter printer = new MessagePrinter(writer);

        MessageHandler[] handlers =
        {
            printer.WritePlain,
            printer.WriteInFrame,
            printer.WriteWithTime,
            printer.WriteUpper
        };

        foreach (MessageHandler handler in handlers)
        {
            handler(text);
        }

        AppendLine("--- Усі способи (масив делегатів) ---");
        AppendText(writer.ToString());
    }

    private void ClearButton_Click(object? sender, EventArgs e)
    {
        outputBox.Clear();
    }

    private void ShowOne(string title, Func<MessagePrinter, MessageHandler> selectMethod)
    {
        if (!TryGetMessage(out string text))
        {
            return;
        }

        StringWriter writer = new StringWriter();
        MessagePrinter printer = new MessagePrinter(writer);

        MessageHandler handler = selectMethod(printer);
        handler(text);

        AppendLine("--- " + title + " ---");
        AppendText(writer.ToString());
    }

    private bool TryGetMessage(out string text)
    {
        text = messageBox.Text.Trim();

        if (text.Length == 0)
        {
            MessageBox.Show("Спочатку введіть повідомлення.", "Порожнє повідомлення",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        return true;
    }

    private void AppendLine(string line)
    {
        AppendText(line + Environment.NewLine);
    }

    private void AppendText(string text)
    {
        outputBox.AppendText(text);
    }
}
