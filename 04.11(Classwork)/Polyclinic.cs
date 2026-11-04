namespace Primer;

public class Patient
{
    public string Name { get; }

    public Patient(string name)
    {
        Name = name;
    }
}

public class Polyclinic
{
    public string DoctorName { get; }

    private readonly Queue<Patient> queue = new();

    public int Count => queue.Count;

    public Polyclinic(string doctorName)
    {
        DoctorName = doctorName;
    }

    public void AddPatient(Patient patient)
    {
        bool threeWaiting = queue.Count == 3;
        queue.Enqueue(patient);

        if (threeWaiting)
        {
            Console.WriteLine($"  {patient.Name} став у кінець черги — там уже чекають троє (усього: {queue.Count})");
        }
        else
        {
            Console.WriteLine($"  {patient.Name} став у чергу (у черзі: {queue.Count})");
        }
    }

    public Patient CallNext()
    {
        Patient patient = queue.Dequeue();
        Console.WriteLine($"  {DoctorName} приймає {patient.Name} (у черзі залишилось: {queue.Count})");
        return patient;
    }

    public void PrintQueue()
    {
        if (queue.Count == 0)
        {
            Console.WriteLine("Черга: порожньо");
            return;
        }

        Console.WriteLine("Черга: " + string.Join(" -> ", queue.Select(p => p.Name)) + $" ({queue.Count})");
    }
}
