using System;

public class Address
{
    private readonly string _street;
    private readonly string _city;
    private readonly string _zipCode;

    public Address(string street, string city, string zipCode)
    {
        _street = Guard.RequireText(street, nameof(street), "Вулиця");
        _city = Guard.RequireText(city, nameof(city), "Місто");
        _zipCode = ValidateZipCode(zipCode);
    }

    public string Street
    {
        get
        {
            return _street;
        }
    }

    public string City
    {
        get
        {
            return _city;
        }
    }

    public string ZipCode
    {
        get
        {
            return _zipCode;
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Адреса: {_street}, м. {_city}, {_zipCode}");
    }

    private static string ValidateZipCode(string zipCode)
    {
        if (zipCode == null)
        {
            throw new ArgumentNullException(nameof(zipCode), "Індекс не може бути null.");
        }

        string code = zipCode.Trim();
        if (code.Length != 5)
        {
            throw new ArgumentException("Індекс повинен містити 5 цифр.", nameof(zipCode));
        }

        foreach (char symbol in code)
        {
            if (!char.IsDigit(symbol))
            {
                throw new ArgumentException("Індекс повинен містити лише цифри.", nameof(zipCode));
            }
        }

        return code;
    }
}
