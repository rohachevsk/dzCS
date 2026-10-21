using System;

public class Article : Document
{
    public string Category { get; set; }

    public int ReadTime { get; set; }

    public Article(string title, string author, string text, string category, int readTime)
        : base(title, author, text)
    {
        Category = category;
        ReadTime = readTime;
    }

    public override string GetInfo()
    {
        return base.GetInfo() + Environment.NewLine +
               "Тип: стаття" + Environment.NewLine +
               "Категорія: " + Category + Environment.NewLine +
               "Час читання: " + ReadTime + " хв";
    }
}
