namespace Primer;

/// <summary>Дані подій поповнення, витрати коштів та старту кредитних коштів.</summary>
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

/// <summary>Дані події досягнення заданої суми грошей.</summary>
public class LimitReachedEventArgs : EventArgs
{
    public decimal AlertSum { get; }
    public decimal BalanceAfter { get; }

    public LimitReachedEventArgs(decimal alertSum, decimal balanceAfter)
    {
        AlertSum = alertSum;
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
/// Завдання 2: кредитна картка з подіями про поповнення рахунку,
/// витрату коштів, старт використання кредитних коштів,
/// досягнення ліміту заданої суми грошей та зміну PIN.
/// </summary>
public class CreditCard
{
    public string CardNumber { get; }
    public string Owner { get; }
    public DateTime ExpiryDate { get; }
    public decimal CreditLimit { get; }

    /// <summary>Сума на рахунку. Від'ємне значення — використовуються кредитні кошти.</summary>
    public decimal Balance { get; private set; }

    /// <summary>Задана сума, при досягненні якої спрацьовує подія LimitReached.</summary>
    public decimal AlertSum { get; }

    private string pin;
    private bool creditStarted;   // щоб подія старту кредитних коштів спрацювала один раз
    private bool limitNotified;   // щоб подія досягнення суми не повторювалась

    /// <summary>Поповнення рахунку.</summary>
    public event EventHandler<CardOperationEventArgs>? Deposited;

    /// <summary>Витрата коштів з рахунку.</summary>
    public event EventHandler<CardOperationEventArgs>? Withdrawn;

    /// <summary>Старт використання кредитних коштів (баланс вперше став від'ємним).</summary>
    public event EventHandler<CardOperationEventArgs>? CreditStarted;

    /// <summary>Досягнення ліміту заданої суми грошей (баланс опустився до AlertSum).</summary>
    public event EventHandler<LimitReachedEventArgs>? LimitReached;

    /// <summary>Зміна PIN.</summary>
    public event EventHandler<PinChangedEventArgs>? PinChanged;

    public CreditCard(string cardNumber, string owner, DateTime expiryDate,
        string pin, decimal creditLimit, decimal alertSum, decimal startBalance = 0)
    {
        CardNumber = cardNumber;
        Owner = owner;
        ExpiryDate = expiryDate;
        this.pin = pin;
        CreditLimit = creditLimit;
        AlertSum = alertSum;
        Balance = startBalance;
    }

    /// <summary>Поповнити рахунок на задану суму.</summary>
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума поповнення має бути додатною.", nameof(amount));
        }

        Balance += amount;
        Deposited?.Invoke(this, new CardOperationEventArgs(amount, Balance));
    }

    /// <summary>
    /// Витратити кошти (перевіряється PIN). Можна використовувати власні та кредитні
    /// кошти, але не більше кредитного ліміту. Повертає true, якщо операція пройшла.
    /// </summary>
    public bool Withdraw(decimal amount, string pin)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума витрати має бути додатною.", nameof(amount));
        }

        if (this.pin != pin)
        {
            return false;                                  // неправильний PIN
        }

        if (Balance - amount < -CreditLimit)
        {
            return false;                                  // вихід за кредитний ліміт
        }

        Balance -= amount;
        Withdrawn?.Invoke(this, new CardOperationEventArgs(amount, Balance));

        if (Balance < 0 && creditStarted == false)
        {
            creditStarted = true;                          // уперше взяли кредитні кошти
            CreditStarted?.Invoke(this, new CardOperationEventArgs(amount, Balance));
        }

        if (Balance <= AlertSum && limitNotified == false)
        {
            limitNotified = true;                          // досягли заданої суми
            LimitReached?.Invoke(this, new LimitReachedEventArgs(AlertSum, Balance));
        }

        return true;
    }

    /// <summary>Змінити PIN. Повертає true, якщо старий PIN указано правильно.</summary>
    public bool ChangePin(string oldPin, string newPin)
    {
        if (this.pin != oldPin)
        {
            return false;
        }

        this.pin = newPin;
        PinChanged?.Invoke(this, new PinChangedEventArgs(oldPin, newPin));
        return true;
    }

    public override string ToString()
    {
        return $"Карта {CardNumber} ({Owner}), діє до {ExpiryDate:dd.MM.yyyy}, " +
               $"баланс: {Balance}, кредитний ліміт: {CreditLimit}";
    }
}
