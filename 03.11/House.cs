namespace Primer;

using System.Collections;
using System.Collections.Generic;

public class Resident
{
    public string FullName { get; set; }
    public int Age { get; set; }

    public Resident(string fullName, int age)
    {
        FullName = fullName;
        Age = age;
    }

    public override string ToString()
    {
        return $"{FullName}, {Age} р.";
    }
}

public class Apartment : IEnumerable<Resident>
{
    public int Number { get; set; }
    public int RoomsCount { get; set; }
    private readonly List<Resident> residents = new();

    public Apartment(int number, int roomsCount)
    {
        Number = number;
        RoomsCount = roomsCount;
    }

    public int ResidentsCount => residents.Count;

    public void AddResident(Resident resident)
    {
        residents.Add(resident);
    }

    public void AddResident(string fullName, int age)
    {
        residents.Add(new Resident(fullName, age));
    }

    public IEnumerator<Resident> GetEnumerator()
    {
        foreach (var resident in residents)
        {
            yield return resident;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"Квартира №{Number} ({RoomsCount}-кімн.), мешканців: {residents.Count}";
    }
}

public class House : IEnumerable<Apartment>
{
    public string Address { get; set; }
    private readonly List<Apartment> apartments = new();

    public House(string address)
    {
        Address = address;
    }

    public int ApartmentsCount => apartments.Count;

    public void AddApartment(Apartment apartment)
    {
        apartments.Add(apartment);
    }

    public IEnumerator<Apartment> GetEnumerator()
    {
        foreach (var apartment in apartments)
        {
            yield return apartment;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"Будинок за адресою: {Address}, квартир: {apartments.Count}";
    }
}

