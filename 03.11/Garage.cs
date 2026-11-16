namespace Primer;

using System.Collections;
using System.Collections.Generic;

public class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; }
    public string Color { get; set; }

    public Car(string brand, string model, int year, string licensePlate, string color)
    {
        Brand = brand;
        Model = model;
        Year = year;
        LicensePlate = licensePlate;
        Color = color;
    }

    public override string ToString()
    {
        return $"{Brand} {Model} ({Year} р.), колір: {Color}, номер: {LicensePlate}";
    }
}

public class Garage : IEnumerable<Car>
{
    public string Name { get; set; }
    public string Address { get; set; }
    private readonly List<Car> cars = new();

    public Garage(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public int CarsCount => cars.Count;

    public void AddCar(Car car)
    {
        cars.Add(car);
    }

    public void AddCar(string brand, string model, int year, string licensePlate, string color)
    {
        cars.Add(new Car(brand, model, year, licensePlate, color));
    }

    public IEnumerator<Car> GetEnumerator()
    {
        foreach (var car in cars)
        {
            yield return car;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override string ToString()
    {
        return $"Гараж \"{Name}\" ({Address}), автомобілів: {cars.Count}";
    }
}

