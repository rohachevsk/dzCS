namespace Delegate;

/// <summary>Дані подій поповнення та витрати коштів.</summary>
public class CardOperationEventArgs : EventArgs
{
    public decimal Amount { get; }
    public decimal BalanceAfter { get; }

    public CardOperationEventArgs(decimal amount, decimal balanceAfter)
    {
        Amount = amount;
        BalanceAfter = balanceAfter;
    }
}

/// <summary>Дані події зміни PIN.</summary>
public class PinChangedEventArgs : EventArgs
{
    public string OldPin { get; }
    public string NewPin { get; }

    public PinChangedEventArgs(string oldPin, string newPin)
    {
        OldPin = oldPin;
        NewPin = newPin;
    }
}

/// <summary>
/// Завдання 3: кредитна картка з подіями:
/// поповнення, витрата, старт кредитних коштів,
/// досягнення кредитного ліміту, зміна PIN.
/// </summary>
public class CreditCard
{
    public string CardNumber { get; }
    public string Owner { get; }
    public DateTime ExpiryDate { get; }
    public decimal CreditLimit { get; }

    /// <summary>Сума грошей на рахунку. Може бути від'ємною в межах кредитного ліміту.</summary>
    public decimal Balance { get; private set; }

    private string pin;

    /// <summary>Поповнення рахунку.</summary>
    public event EventHandler<CardOperationEventArgs>? Deposited;

    /// <summary>Витрата коштів з рахунку.</summary>
    public event EventHandler<CardOperationEventArgs>? Withdrawn;

    /// <summary>Старт використання кредитних коштів (баланс вперше пішов у мінус).</summary>
    public event EventHandler? CreditStarted;

    /// <summary>Досягнення кредитного ліміту.</summary>
    public event EventHandler? CreditLimitReached;

    /// <summary>Зміна PIN.</summary>
    public event EventHandler<PinChangedEventArgs>? PinChanged;

    public CreditCard(
        string cardNumber,
        string owner,
        DateTime expiryDate,
        string pin,
        decimal creditLimit,
        decimal balance = 0m)
    {
        CardNumber = cardNumber;
        Owner = owner;
        ExpiryDate = expiryDate;
        this.pin = pin;
        CreditLimit = creditLimit;
        Balance = balance;
    }

    public bool CheckPin(string value)
    {
        return pin == value;
    }

    /// <summary>Поповнити рахунок. Повертає false, якщо сума не додатна.</summary>
    public bool Deposit(decimal amount)
    {
        if (amount <= 0m)
        {
            return false;
        }

        Balance += amount;
        Deposited?.Invoke(this, new CardOperationEventArgs(amount, Balance));
        return true;
    }

    /// <summary>
    /// Списати кошти. Можна витратити власні гроші та кредит у межах ліміту.
    /// Повертає false, якщо сума не додатна або перевищує доступні кошти.
    /// </summary>
    public bool Withdraw(decimal amount)
    {
        if (amount <= 0m || amount > Balance + CreditLimit)
        {
            return false;
        }

        decimal before = Balance;
        Balance -= amount;

        Withdrawn?.Invoke(this, new CardOperationEventArgs(amount, Balance));

        if (before >= 0m && Balance < 0m)
        {
            CreditStarted?.Invoke(this, EventArgs.Empty);
        }

        if (Balance <= -CreditLimit)
        {
            CreditLimitReached?.Invoke(this, EventArgs.Empty);
        }

        return true;
    }

    /// <summary>
    /// Змінити PIN. Потрібен правильний старий PIN,
    /// новий PIN має бути не порожнім і відрізнятися від старого.
    /// </summary>
    public bool ChangePin(string oldPin, string newPin)
    {
        if (pin != oldPin || newPin == oldPin || string.IsNullOrEmpty(newPin))
        {
            return false;
        }

        string previous = pin;
        pin = newPin;
        PinChanged?.Invoke(this, new PinChangedEventArgs(previous, newPin));
        return true;
    }
}
