namespace Centeva.DomainModeling.SampleApp.BankAccounts;

/// <summary>
/// Represents an amount of money in a specific currency.
/// </summary>
public readonly record struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency) || currency.Trim().Length != 3 || !currency.All(char.IsLetter))
            throw new ArgumentException("Currency must be a 3-letter ISO 4217 code (e.g. USD).", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public static Money Zero(string currency) => new(0m, currency);

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount - other.Amount, Currency);
    }

    public bool IsNegative => Amount < 0m;

    public override string ToString() => $"{Amount:F2} {Currency}";

    private void EnsureSameCurrency(Money other)
    {
        if (!string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException(
                $"Cannot operate on amounts with different currencies: {Currency} vs {other.Currency}.");
    }
}
