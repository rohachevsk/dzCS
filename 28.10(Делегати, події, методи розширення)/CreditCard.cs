namespace Primer;

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

public class CreditCard
{
    public string CardNumber { get; }
    public string Owner { get; }
    public DateTime ExpiryDate { get; }
    public decimal CreditLimit { get; }
    public decimal Balance { get; private set; }
    public decimal AlertSum { get; }

    private string pin;
    private bool creditStarted;
    private bool limitNotified;

    public event EventHandler<CardOperationEventArgs>? Deposited;
    public event EventHandler<CardOperationEventArgs>? Withdrawn;
    public event EventHandler<CardOperationEventArgs>? CreditStarted;
    public event EventHandler<LimitReachedEventArgs>? LimitReached;
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

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума поповнення має бути додатною.", nameof(amount));
        }

        Balance += amount;
        Deposited?.Invoke(this, new CardOperationEventArgs(amount, Balance));
    }

    public bool Withdraw(decimal amount, string pin)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума витрати має бути додатною.", nameof(amount));
        }

        if (this.pin != pin)
        {
            return false;
        }

        if (Balance - amount < -CreditLimit)
        {
            return false;
        }

        Balance -= amount;
        Withdrawn?.Invoke(this, new CardOperationEventArgs(amount, Balance));

        if (Balance < 0 && creditStarted == false)
        {
            creditStarted = true;
            CreditStarted?.Invoke(this, new CardOperationEventArgs(amount, Balance));
        }

        if (Balance <= AlertSum && limitNotified == false)
        {
            limitNotified = true;
            LimitReached?.Invoke(this, new LimitReachedEventArgs(AlertSum, Balance));
        }

        return true;
    }

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
