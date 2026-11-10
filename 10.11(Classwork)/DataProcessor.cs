namespace Primer;

/// <summary>
/// Клас для обробки великих обсягів даних відповідно до умови завдання.
/// </summary>
public class DataProcessor
{
    private byte[] _buffer;

    public DataProcessor(int size)
    {
        _buffer = new byte[size];
    }

    /// <summary>
    /// Доступ до внутрішнього буфера для перевірки його покоління в GC.
    /// </summary>
    public byte[] Buffer => _buffer;

    public void Process()
    {
        // Імітація обробки даних: записуємо значення в початок і кінець буфера
        if (_buffer.Length > 0)
        {
            _buffer[0] = 42;
            _buffer[^1] = 99;
        }
    }
}

/// <summary>
/// Допоміжний клас із фіналізатором для демонстрації GC.WaitForPendingFinalizers().
/// </summary>
public class FinalizableResource
{
    public static int FinalizedCount = 0;
    public int Id { get; }

    public FinalizableResource(int id)
    {
        Id = id;
    }

    ~FinalizableResource()
    {
        Interlocked.Increment(ref FinalizedCount);
    }
}

