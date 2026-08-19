namespace Centeva.DomainModeling.SampleApp.BankAccounts;

/// <summary>
/// Human-readable unique identifier for a <see cref="BankAccount"/>, suitable for display to customers.
/// </summary>
public readonly record struct AccountNumber(string Value)
{
    public static AccountNumber From(AccountId id) =>
        new($"ACC-{id.Value.ToString("N").ToUpperInvariant()}");

    public override string ToString() => Value;
}
