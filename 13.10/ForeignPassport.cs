using System;
using System.Collections.Generic;

public class ForeignPassport
{
    public const int MaxValidityYears = 10;
    public const int MaxPassportNumberLength = 8;

    private static readonly HashSet<string> ValidCountryCodes;
    private static int _createdCount;

    private readonly Guid _id;

    static ForeignPassport()
    {
        ValidCountryCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "UKR", "POL", "DEU", "FRA", "USA", "GBR", "CZE", "CAN"
        };

        if (ValidCountryCodes.Count == 0)
        {
            throw new InvalidOperationException("Довідник кодів країн не ініціалізовано.");
        }
    }

    public ForeignPassport()
    {
        _id = Guid.NewGuid();
        PassportNumber = "AA000000";
        FullName = "Іваненко Іван Іванович";
        BirthDate = new DateTime(1990, 1, 1);
        IssueDate = new DateTime(2020, 1, 1);
        ExpiryDate = new DateTime(2030, 1, 1);
        IssuingAuthority = "Державна міграційна служба України";
        CountryCode = "UKR";

        _createdCount++;
    }

    public ForeignPassport(
        string passportNumber,
        string fullName,
        DateTime birthDate,
        DateTime issueDate,
        DateTime expiryDate,
        string issuingAuthority,
        string countryCode)
    {
        _id = Guid.NewGuid();
        PassportNumber = ValidatePassportNumber(passportNumber);
        FullName = ValidateFullName(fullName);
        BirthDate = birthDate;
        IssueDate = issueDate;
        ExpiryDate = expiryDate;
        IssuingAuthority = ValidateIssuingAuthority(issuingAuthority);
        CountryCode = ValidateCountryCode(countryCode);

        ValidateDates(birthDate, issueDate, expiryDate);
        _createdCount++;
    }

    public Guid Id
    {
        get
        {
            return _id;
        }
    }

    public string PassportNumber { get; private set; }

    public string FullName { get; private set; }

    public DateTime BirthDate { get; private set; }

    public DateTime IssueDate { get; private set; }

    public DateTime ExpiryDate { get; private set; }

    public string IssuingAuthority { get; private set; }

    public string CountryCode { get; private set; }

    public static int CreatedCount
    {
        get
        {
            return _createdCount;
        }
    }

    public static bool IsCountryCodeValid(string countryCode)
    {
        return countryCode != null && ValidCountryCodes.Contains(countryCode.Trim());
    }

    private static string ValidatePassportNumber(string passportNumber)
    {
        if (passportNumber == null)
        {
            throw new ArgumentNullException(nameof(passportNumber), "Номер паспорта не може бути null.");
        }

        string number = passportNumber.Trim();
        if (number.Length != MaxPassportNumberLength)
        {
            throw new ArgumentException(
                $"Номер паспорта повинен містити {MaxPassportNumberLength} символів: 2 літери та 6 цифр.",
                nameof(passportNumber));
        }

        for (int i = 0; i < 2; i++)
        {
            if (!char.IsLetter(number[i]))
            {
                throw new ArgumentException("Номер паспорта повинен починатися з двох літер.", nameof(passportNumber));
            }
        }

        for (int i = 2; i < number.Length; i++)
        {
            if (!char.IsDigit(number[i]))
            {
                throw new ArgumentException("Після двох літер у номері паспорта повинні йти цифри.", nameof(passportNumber));
            }
        }

        return number.ToUpperInvariant();
    }

    private static string ValidateFullName(string fullName)
    {
        if (fullName == null)
        {
            throw new ArgumentNullException(nameof(fullName), "ПІБ не може бути null.");
        }

        string name = fullName.Trim();
        if (name.Length == 0)
        {
            throw new ArgumentException("ПІБ не може бути порожнім.", nameof(fullName));
        }

        string[] parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2)
        {
            throw new ArgumentException("ПІБ повинно містити щонайменше прізвище та ім'я.", nameof(fullName));
        }

        foreach (char symbol in name)
        {
            if (char.IsDigit(symbol))
            {
                throw new ArgumentException("ПІБ не може містити цифри.", nameof(fullName));
            }
        }

        return name;
    }

    private static string ValidateIssuingAuthority(string issuingAuthority)
    {
        if (issuingAuthority == null)
        {
            throw new ArgumentNullException(nameof(issuingAuthority), "Назва органу не може бути null.");
        }

        string authority = issuingAuthority.Trim();
        if (authority.Length == 0)
        {
            throw new ArgumentException("Назва органу, який видав паспорт, не може бути порожньою.", nameof(issuingAuthority));
        }

        return authority;
    }

    private static string ValidateCountryCode(string countryCode)
    {
        if (countryCode == null)
        {
            throw new ArgumentNullException(nameof(countryCode), "Код країни не може бути null.");
        }

        string code = countryCode.Trim().ToUpperInvariant();
        if (!ValidCountryCodes.Contains(code))
        {
            throw new ArgumentException("Неприпустимий код країни: " + countryCode, nameof(countryCode));
        }

        return code;
    }

    private static void ValidateDates(DateTime birthDate, DateTime issueDate, DateTime expiryDate)
    {
        if (birthDate > DateTime.Today)
        {
            throw new ArgumentOutOfRangeException(nameof(birthDate), "Дата народження не може бути в майбутньому.");
        }

        if (birthDate > issueDate)
        {
            throw new ArgumentException("Дата народження не може бути пізнішою за дату видачі паспорта.");
        }

        if (expiryDate < issueDate)
        {
            throw new ArgumentException("Дата закінчення терміну дії не може передувати даті видачі паспорта.");
        }

        if (expiryDate > issueDate.AddYears(MaxValidityYears))
        {
            throw new ArgumentOutOfRangeException(
                nameof(expiryDate),
                $"Термін дії паспорта не може перевищувати {MaxValidityYears} років.");
        }
    }

    public override string ToString()
    {
        return $"ID: {Id}{Environment.NewLine}" +
               $"Номер паспорта: {PassportNumber}{Environment.NewLine}" +
               $"ПІБ: {FullName}{Environment.NewLine}" +
               $"Дата народження: {BirthDate:dd.MM.yyyy}{Environment.NewLine}" +
               $"Дата видачі: {IssueDate:dd.MM.yyyy}{Environment.NewLine}" +
               $"Дійсний до: {ExpiryDate:dd.MM.yyyy}{Environment.NewLine}" +
               $"Орган, що видав: {IssuingAuthority}{Environment.NewLine}" +
               $"Код країни: {CountryCode}";
    }
}
