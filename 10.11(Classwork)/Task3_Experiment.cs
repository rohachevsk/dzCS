using System.Diagnostics;

namespace Primer;

public static class Task3Experiment
{
    public static void Run()
    {
        Console.WriteLine("================================================================================");
        Console.WriteLine("ЗАВДАННЯ 3: Практичний експеримент (10 000 об'єктів по 100 KB)");
        Console.WriteLine("================================================================================");
        Console.WriteLine();

        const int objectCount = 10_000;
        const int objectSizeBytes = 100 * 1024; // 100 KB = 102 400 байт (> 85 000 байт, тому це LOH!)

        Console.WriteLine($"Параметри експерименту:");
        Console.WriteLine($"  Кількість об'єктів:    {objectCount:N0}");
        Console.WriteLine($"  Розмір одного об'єкта: {objectSizeBytes:N0} байт ({objectSizeBytes / 1024.0:F1} KB)");
        Console.WriteLine($"  Загальний обсяг даних: {(long)objectCount * objectSizeBytes:N0} байт ({(long)objectCount * objectSizeBytes / (1024.0 * 1024.0):F2} МБ)");
        Console.WriteLine();

        // 0. Очищення перед початком вимірювань для "чистого" старту
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memInitial = GC.GetTotalMemory(false);
        int gen0Initial = GC.CollectionCount(0);
        int gen1Initial = GC.CollectionCount(1);
        int gen2Initial = GC.CollectionCount(2);

        Console.WriteLine("--- Початковий стан пам'яті та лічильників GC ---");
        Console.WriteLine($"Пам'ять: {FormatBytes(memInitial)}");
        Console.WriteLine($"Лічильники GC: Gen 0 = {gen0Initial}, Gen 1 = {gen1Initial}, Gen 2 = {gen2Initial}");
        Console.WriteLine();

        Console.WriteLine(">> Створюємо 10 000 об'єктів DataProcessor (по 100 KB кожен)...");
        Stopwatch sw = Stopwatch.StartNew();

        CreateObjects(objectCount, objectSizeBytes);

        sw.Stop();
        Console.WriteLine($">> Створення завершено за {sw.ElapsedMilliseconds} мс.");
        Console.WriteLine();

        // 1. Стан ДО явного виклику GC
        long memBeforeExplicitGc = GC.GetTotalMemory(false);
        int gen0Before = GC.CollectionCount(0);
        int gen1Before = GC.CollectionCount(1);
        int gen2Before = GC.CollectionCount(2);

        // 2. Викликаємо GC.Collect()
        Console.WriteLine(">> Викликаємо GC.Collect() + GC.WaitForPendingFinalizers() + GC.Collect()...");
        Stopwatch gcSw = Stopwatch.StartNew();

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        gcSw.Stop();
        Console.WriteLine($">> Примусове збирання завершено за {gcSw.ElapsedMilliseconds} мс.");
        Console.WriteLine();

        // 3. Стан ПІСЛЯ явного виклику GC
        long memAfterExplicitGc = GC.GetTotalMemory(false);
        int gen0After = GC.CollectionCount(0);
        int gen1After = GC.CollectionCount(1);
        int gen2After = GC.CollectionCount(2);

        // Виводимо порівняльну таблицю
        Console.WriteLine("================================================================================");
        Console.WriteLine("ПОРІВНЯЛЬНІ РЕЗУЛЬТАТИ ЕКСПЕРИМЕНТУ");
        Console.WriteLine("================================================================================");
        Console.WriteLine($"{"Показник",-38} | {"До виклику GC",-18} | {"Після GC.Collect()",-18}");
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"{"Використання пам'яті (байти)",-38} | {memBeforeExplicitGc,-18:N0} | {memAfterExplicitGc,-18:N0}");
        Console.WriteLine($"{"Використання пам'яті (МБ)",-38} | {memBeforeExplicitGc / (1024.0 * 1024.0),-18:F2} | {memAfterExplicitGc / (1024.0 * 1024.0),-18:F2}");
        Console.WriteLine($"{"Кількість запусків GC Gen 0",-38} | {gen0Before,-18} | {gen0After,-18}");
        Console.WriteLine($"{"Кількість запусків GC Gen 1",-38} | {gen1Before,-18} | {gen1After,-18}");
        Console.WriteLine($"{"Кількість запусків GC Gen 2 (LOH)",-38} | {gen2Before,-18} | {gen2After,-18}");
        Console.WriteLine(new string('-', 80));
        Console.WriteLine($"{"Звільнено пам'яті:",-38} | {FormatBytes(memBeforeExplicitGc - memAfterExplicitGc)}");
        Console.WriteLine($"{"Загалом збирань Gen 0 за експеримент:",-38} | {gen0After - gen0Initial}");
        Console.WriteLine($"{"Загалом збирань Gen 1 за експеримент:",-38} | {gen1After - gen1Initial}");
        Console.WriteLine($"{"Загалом збирань Gen 2 за експеримент:",-38} | {gen2After - gen2Initial}");
        Console.WriteLine("================================================================================");
        Console.WriteLine();

        PrintConclusions();
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
    private static void CreateObjects(int count, int size)
    {
        for (int i = 0; i < count; i++)
        {
            var processor = new DataProcessor(size);
            processor.Process();
        }
    }

    private static void PrintConclusions()
    {
        Console.WriteLine("Висновки за результатами експерименту:");
        Console.WriteLine("1. Використання пам'яті:");
        Console.WriteLine("   • До явного виклику GC.Collect() у купі залишаються тимчасові буфери, які ще");
        Console.WriteLine("     не встиг підчистити фоновий GC.");
        Console.WriteLine("   • Після виклику GC.Collect() використання пам'яті різко знижується майже до початкового рівня,");
        Console.WriteLine("     оскільки всі 10 000 об'єктів були локальними і більше не мають посилань.");
        Console.WriteLine();
        Console.WriteLine("2. Запуски поколінь GC (Особливість LOH):");
        Console.WriteLine("   • Розмір кожного буфера становить 100 KB = 102 400 байт. Це більше за 85 000 байт,");
        Console.WriteLine("     тому масиви потрапляють у Large Object Heap (LOH).");
        Console.WriteLine("   • Збирання LOH відбувається разом із Поколінням 2 (Gen 2). Саме тому ми спостерігаємо");
        Console.WriteLine("     значну кількість спрацювань Gen 2 порівняно зі звичайною роботою програми.");
        Console.WriteLine("   • Часті виділення у LOH створюють значне навантаження на систему, оскільки Gen 2");
        Console.WriteLine("     колекції вимагають найбільше часу та процесорних ресурсів.");
        Console.WriteLine("================================================================================");
    }

    private static string FormatBytes(long bytes)
    {
        return $"{bytes:N0} байт ({bytes / (1024.0 * 1024.0):F2} МБ)";
    }
}

