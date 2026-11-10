namespace Primer;

public static class Task1MemoryAnalysis
{
    public static void Run()
    {
        Console.WriteLine("================================================================================");
        Console.WriteLine("ЗАВДАННЯ 1: Аналіз пам'яті (Memory Analysis)");
        Console.WriteLine("================================================================================");
        Console.WriteLine();

        Console.WriteLine("1. Запуск вихідного фрагмента коду з умови:");
        Console.WriteLine("   Створення 1 000 об'єктів DataProcessor з буфером 1 МБ (1024 * 1024 байт)...");
        Console.WriteLine();

        long memoryBefore = GC.GetTotalMemory(false);
        int gen0Before = GC.CollectionCount(0);
        int gen1Before = GC.CollectionCount(1);
        int gen2Before = GC.CollectionCount(2);

        // Демонстрація для одного об'єкта перед циклом
        var sample = new DataProcessor(1024 * 1024);
        sample.Process();
        int sampleGen = GC.GetGeneration(sample);
        int bufferGen = GC.GetGeneration(sample.Buffer);
        sample = null; // робимо недосяжним

        for (int i = 0; i < 1000; i++)
        {
            var processor = new DataProcessor(1024 * 1024);
            processor.Process();
        }

        long memoryAfterNoGc = GC.GetTotalMemory(false);
        int gen0After = GC.CollectionCount(0);
        int gen1After = GC.CollectionCount(1);
        int gen2After = GC.CollectionCount(2);

        Console.WriteLine($"Результат виконання: Memory: {memoryAfterNoGc:N0} байт ({memoryAfterNoGc / (1024.0 * 1024.0):F2} МБ)");
        Console.WriteLine();

        Console.WriteLine("Статистика запусків GC під час циклу (автоматичні спрацювання):");
        Console.WriteLine($"  Gen 0 збирань: {gen0After - gen0Before}");
        Console.WriteLine($"  Gen 1 збирань: {gen1After - gen1Before}");
        Console.WriteLine($"  Gen 2 збирань: {gen2After - gen2Before}");
        Console.WriteLine();

        Console.WriteLine("Покоління компонентів об'єкта:");
        Console.WriteLine($"  Об'єкт DataProcessor:  Покоління {sampleGen} (маленький об'єкт ~24-32 байти)");
        Console.WriteLine($"  Масив byte[1024*1024]: Покоління {bufferGen} (LOH - Large Object Heap, асоціюється з Gen 2)");
        Console.WriteLine();

        PrintAnswers();
    }

    public static void PrintAnswers()
    {
        Console.WriteLine("--------------------------------------------------------------------------------");
        Console.WriteLine("ВІДПОВІДІ НА ТЕОРЕТИЧНІ ЗАПИТАННЯ:");
        Console.WriteLine("--------------------------------------------------------------------------------");

        Console.WriteLine("1. Що відбувається з об'єктами після кожної ітерації?");
        Console.WriteLine("   • На кожній ітерації виділяється новий об'єкт DataProcessor і масив byte[1048576].");
        Console.WriteLine("   • Посилання зберігається в локальній змінній 'processor'.");
        Console.WriteLine("   • Коли ітерація завершується, змінна 'processor' виходить з області видимості,");
        Console.WriteLine("     і кореневі посилання (GC Roots) на об'єкт втрачаються.");
        Console.WriteLine("   • Об'єкт та його внутрішній буфер стають 'мертвими' (недосяжними / unreachable).");
        Console.WriteLine("   • Вони залишаються у купі пам'яті до наступного спрацювання збирача сміття (GC).");
        Console.WriteLine();

        Console.WriteLine("2. Чому пам'ять не обов'язково звільняється одразу після завершення ітерації?");
        Console.WriteLine("   • Керування пам'яттю в .NET є недетермінованим (non-deterministic).");
        Console.WriteLine("   • GC працює у фоні за потреби і не відстежує кожен вихід з області видимості.");
        Console.WriteLine("   • Збирання сміття є дорогою операцією (зупинка потоків, маркування, підмітання).");
        Console.WriteLine("     Якби GC запускався на кожній ітерації, програма зазнала б величезних втрат швидкодії.");
        Console.WriteLine("   • GC запускається лише коли: а) вичерпано бюджет пам'яті покоління; б) системі бракує RAM;");
        Console.WriteLine("     в) викликано GC.Collect() явно.");
        Console.WriteLine();

        Console.WriteLine("3. Які покоління GC існують у .NET?");
        Console.WriteLine("   • Gen 0 (Покоління 0) — короткоживучі об'єкти (локальні змінні, тимчасові структури).");
        Console.WriteLine("     Найчастіші та найшвидші збирання.");
        Console.WriteLine("   • Gen 1 (Покоління 1) — буфер між коротко- та довгоживучими об'єктами.");
        Console.WriteLine("     Сюди переходять ті, хто пережив збирання в Gen 0.");
        Console.WriteLine("   • Gen 2 (Покоління 2) — довгоживучі об'єкти (статичні дані, кеші тощо).");
        Console.WriteLine("     Повне збирання (Full GC) — найважче та найдовше.");
        Console.WriteLine("   • Спеціальні сегменти купи:");
        Console.WriteLine("     - LOH (Large Object Heap) — для об'єктів розміром >= 85 000 байт (збирається разом із Gen 2).");
        Console.WriteLine("     - POH (Pinned Object Heap, з .NET 5) — для закріплених об'єктів.");
        Console.WriteLine("     - Frozen/Non-GC Heap (з .NET 8) — для незмінних статичних об'єктів.");
        Console.WriteLine();

        Console.WriteLine("4. До якого покоління потраплятимуть створені об'єкти?");
        Console.WriteLine("   • Сам екземпляр DataProcessor має малий розмір (заголовок + посилання ~24-32 байти),");
        Console.WriteLine("     тому він створюється у Поколінні 0 (Gen 0).");
        Console.WriteLine("   • Внутрішній масив _buffer має розмір 1 048 576 байт (1 МБ), що значно перевищує");
        Console.WriteLine("     поріг 85 000 байт. Тому він потрапляє безпосередньо у LOH (Large Object Heap)!");
        Console.WriteLine("   • Оскільки LOH обслуговується разом із Поколінням 2, метод GC.GetGeneration(buffer)");
        Console.WriteLine("     повертає покоління 2. Це спричиняє часті збирання Gen 2 при виділенні великих буферів.");
        Console.WriteLine("================================================================================");
    }
}

