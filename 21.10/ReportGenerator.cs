using System;
using System.Collections.Generic;
using System.Text;

public static class ReportGenerator
{
    public static string GenerateReport(List<Document> documents)
    {
        StringBuilder builder = new StringBuilder();

        builder.AppendLine("ЗВІТ ПРО ДОКУМЕНТИ");
        builder.AppendLine("-------------------");
        builder.AppendLine("Кількість документів: " + documents.Count);
        builder.AppendLine();

        int totalWords = 0;
        int totalCharacters = 0;

        for (int i = 0; i < documents.Count; i++)
        {
            Document document = documents[i];
            DocumentStatistics statistics = document.GetStatistics();

            builder.AppendLine("Документ №" + (i + 1));
            builder.AppendLine(document.GetInfo());
            builder.AppendLine("Статистика тексту:");
            builder.AppendLine("  Кількість слів: " + statistics.WordCount);
            builder.AppendLine("  Кількість символів: " + statistics.CharacterCount);
            builder.AppendLine("  Найдовше слово: " + statistics.LongestWord);
            builder.AppendLine("  Унікальних слів: " + statistics.UniqueWordCount);
            builder.AppendLine();

            totalWords = totalWords + statistics.WordCount;
            totalCharacters = totalCharacters + statistics.CharacterCount;
        }

        builder.AppendLine("ЗАГАЛЬНИЙ ПІДСУМОК");
        builder.AppendLine("Всього документів: " + documents.Count);
        builder.AppendLine("Всього слів: " + totalWords);
        builder.AppendLine("Всього символів: " + totalCharacters);

        return builder.ToString();
    }
}
