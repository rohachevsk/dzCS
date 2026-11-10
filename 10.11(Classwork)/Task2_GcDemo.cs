namespace Primer;

public static class Task2GcDemo
{
    public static void Run()
    {
        Console.WriteLine("================================================================================");
        Console.WriteLine("ЗАВДАННЯ 2: Демонстрація роботи методів Garbage Collector (.NET GC)");
        Console.WriteLine("================================================================================");
        Console.WriteLine();

        // 1. Демонстрація GC.GetGeneration() та підвищення поколінь (Promotion)
        DemoGetGeneration();

        // 2. Демонстрація GC.GetTotalMemory(), GC.Collect(), GC.CollectionCount()
        DemoMemoryAndCollect();

        // 3. Демонстрація GC.WaitForPendingFinalizers()
        DemoFinalizers();

        Console.WriteLine("================================================================================");
    }

    private static void DemoGetGeneration()
    {
        Console.WriteLine("--- 1. Демонстрація GC.GetGeneration() та переміщення між поколіннями ---");
        Console.WriteLine($"Максимальне підтримуване покоління в .NET: {GC.MaxGeneration}");

        // Створюємо маленький довгоживучий об'єкт (зберігаємо посилання)
        object survivor = new object();
        Console.WriteLine($"1) Створено об'єкт 'survivor'.");
        Console.WriteLine($"   GC.GetGeneration(survivor) = {GC.GetGeneration(survivor)} (початково Gen 0)");

        // Примусово збираємо Gen 0, оскільки на 'survivor' є посилання, він виживає і переходить у Gen 1
        GC.Collect(0, GCCollectionMode.Forced);
        Console.WriteLine($"2) Після GC.Collect(0) об'єкт вижив:");
        Console.WriteLine($"   GC.GetGeneration(survivor) = {GC.GetGeneration(survivor)} (підвищено до Gen 1)");

        // Примусово збираємо Gen 1, об'єкт знову виживає і переходить у Gen 2
        GC.Collect(1, GCCollectionMode.Forced);
        Console.WriteLine($"3) Після GC.Collect(1) об'єкт знову вижив:");
        Console.WriteLine($"   GC.GetGeneration(survivor) = {GC.GetGeneration(survivor)} (підвищено до Gen 2)");

        // Покажемо, що великий масив одразу потрапляє у Gen 2 (LOH)
        byte[] largeArray = new byte[100 * 1024]; // 100 KB > 85 KB
        Console.WriteLine($"4) Створено масив 100 KB (> 85 KB):");
        Console.WriteLine($"   GC.GetGeneration(largeArray) = {GC.GetGeneration(largeArray)} (одразу LOH / Gen 2)");

        // Щоб компілятор не оптимізував змінні завчасно
        GC.KeepAlive(survivor);
        GC.KeepAlive(largeArray);
        Console.WriteLine();
    }

    private static void DemoMemoryAndCollect()
    {
        Console.WriteLine("--- 2. Демонстрація GC.GetTotalMemory(), GC.Collect() та GC.CollectionCount() ---");

        // Початковий стан
        long memStart = GC.GetTotalMemory(false);
        int c0Before = GC.CollectionCount(0);
        int c1Before = GC.CollectionCount(1);
        int c2Before = GC.CollectionCount(2);

        PrintGcStats("ДО створення сміття", memStart, c0Before, c1Before, c2Before);

        // Виділяємо тимчасові об'єкти (сміття)
        AllocateGarbage();

        long memAfterAlloc = GC.GetTotalMemory(false);
        int c0AfterAlloc = GC.CollectionCount(0);
        int c1AfterAlloc = GC.CollectionCount(1);
        int c2AfterAlloc = GC.CollectionCount(2);

        PrintGcStats("ПІСЛЯ створення 20 МБ сміття (ДО виклику GC.Collect)", memAfterAlloc, c0AfterAlloc, c1AfterAlloc, c2AfterAlloc);
        Console.WriteLine($"   [+] Пам'ять зросла на: {(memAfterAlloc - memStart):N0} байт ({(memAfterAlloc - memStart) / (1024.0 * 1024.0):F2} МБ)");

        // Викликаємо GC.Collect()
        Console.WriteLine("\n>> Викликаємо GC.Collect() та очікуємо завершення...");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memAfterGc = GC.GetTotalMemory(false);
        int c0AfterGc = GC.CollectionCount(0);
        int c1AfterGc = GC.CollectionCount(1);
        int c2AfterGc = GC.CollectionCount(2);

        PrintGcStats("ПІСЛЯ явного виклику GC.Collect()", memAfterGc, c0AfterGc, c1AfterGc, c2AfterGc);
        Console.WriteLine($"   [-] Звільнено пам'яті: {(memAfterAlloc - memAfterGc):N0} байт ({(memAfterAlloc - memAfterGc) / (1024.0 * 1024.0):F2} МБ)");
        Console.WriteLine();
    }

    private static void DemoFinalizers()
    {
        Console.WriteLine("--- 3. Демонстрація GC.WaitForPendingFinalizers() ---");
        FinalizableResource.FinalizedCount = 0;

        const int count = 5;
        Console.WriteLine($"Створюємо {count} об'єктів з фіналізатором (~FinalizableResource)...");
        for (int i = 0; i < count; i++)
        {
            _ = new FinalizableResource(i + 1);
        }

        Console.WriteLine($"Кількість виконаних фіналізаторів до GC: {FinalizableResource.FinalizedCount}");

        Console.WriteLine("Викликаємо GC.Collect()...");
        GC.Collect();

        Console.WriteLine($"Фіналізатори одразу після GC.Collect (до очікування): {FinalizableResource.FinalizedCount}");
        Console.WriteLine("Викликаємо GC.WaitForPendingFinalizers()...");
        GC.WaitForPendingFinalizers();

        Console.WriteLine($"Фіналізатори ПІСЛЯ WaitForPendingFinalizers(): {FinalizableResource.FinalizedCount} з {count}");
        Console.WriteLine("-> Пояснення: фіналізатори виконуються в окремому низькопріоритетному потоці.");
        Console.WriteLine("   WaitForPendingFinalizers() блокує поточний потік, доки черга фіналізації не спорожніє.");
        Console.WriteLine();
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
    private static void AllocateGarbage()
    {
        // Створюємо масив байтів загальним обсягом близько 20 МБ
        for (int i = 0; i < 20; i++)
        {
            byte[] garbage = new byte[1024 * 1024];
            garbage[0] = (byte)i;
        }
    }

    private static void PrintGcStats(string stage, long memoryBytes, int gen0, int gen1, int gen2)
    {
        Console.WriteLine($"\n[{stage}]");
        Console.WriteLine($"  Використано пам'яті (GetTotalMemory): {memoryBytes:N0} байт ({memoryBytes / (1024.0 * 1024.0):F2} МБ)");
        Console.WriteLine($"  Кількість запусків GC: Gen 0 = {gen0}, Gen 1 = {gen1}, Gen 2 = {gen2}");
    }
}

