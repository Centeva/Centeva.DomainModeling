namespace Centeva.DomainModeling.SampleApp.BankAccounts;

/// <summary>
/// Strongly typed identifier for a <see cref="BankAccount"/>.
/// </summary>
public readonly record struct AccountId(Guid Value)
{
    public static AccountId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
