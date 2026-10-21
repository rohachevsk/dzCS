using System;

public class Report : Document
{
    public string Department { get; set; }

    public int PageCount { get; set; }

    public Report(string title, string author, string text, string department, int pageCount)
        : base(title, author, text)
    {
        Department = department;
        PageCount = pageCount;
    }

    public override string GetInfo()
    {
        return base.GetInfo() + Environment.NewLine +
               "Тип: звіт" + Environment.NewLine +
               "Відділ: " + Department + Environment.NewLine +
               "Сторінок: " + PageCount;
    }
}
