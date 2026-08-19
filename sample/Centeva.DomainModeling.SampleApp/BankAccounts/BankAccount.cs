namespace Centeva.DomainModeling.SampleApp.BankAccounts;

/// <summary>
/// Aggregate root representing a bank account.
/// Demonstrates: typed ID, complex type value object, domain events, and invariant enforcement.
/// </summary>
/// <remarks>
/// <see cref="Id"/> is the internal surrogate key used for persistence and relationships.
/// <see cref="AccountNumber"/> is the customer-facing alternate key, derived from <see cref="Id"/>.
/// In a production system these would typically be generated independently (e.g. via a database
/// sequence) so that account numbers can be short and human-friendly without being coupled to the
/// internal identifier.
/// </remarks>
public class BankAccount : BaseEntity<AccountId>, IAggregateRoot
{

    /// <summary>
    /// Opens a new bank account with an initial deposit.
    /// </summary>
    public static BankAccount Open(CustomerName ownerName, Money initialDeposit)
    {
        ArgumentNullException.ThrowIfNull(ownerName);
        if (initialDeposit.IsNegative)
        {
            throw new ArgumentException("Initial deposit cannot be negative.", nameof(initialDeposit));
        }

        var id = AccountId.New();
        var account = new BankAccount
        {
            Id = id,
            AccountNumber = AccountNumber.From(id),
            OwnerName = ownerName,
            Balance = initialDeposit,
        };

        account.RegisterDomainEvent(new AccountOpenedEvent(account.Id, ownerName, initialDeposit));
        return account;
    }

    private BankAccount() { } // For EF

    public CustomerName OwnerName { get; private set; } = null!;

    /// <summary>
    /// Human-readable unique identifier for this account, suitable for display to customers.
    /// </summary>
    public AccountNumber AccountNumber { get; private set; }

    public bool IsClosed { get; private set; }

    /// <summary>
    /// Current balance.
    /// </summary>
    public Money Balance { get; private set; }

    /// <summary>
    /// Deposit funds into the account.
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Deposit(Money amount)
    {
        EnsureOpen();
        if (amount.IsNegative)
        {
            throw new ArgumentException("Deposit amount cannot be negative.", nameof(amount));
        }

        Balance = Balance.Add(amount);
        RegisterDomainEvent(new FundsDepositedEvent(Id, amount));
    }

    /// <summary>
    /// Withdraw funds from the account.
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void Withdraw(Money amount)
    {
        EnsureOpen();
        if (amount.IsNegative)
        {
            throw new ArgumentException("Withdrawal amount cannot be negative.", nameof(amount));
        }

        var newBalance = Balance.Subtract(amount);
        if (newBalance.IsNegative)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance = newBalance;
        RegisterDomainEvent(new FundsWithdrawnEvent(Id, amount));
    }

    /// <summary>
    /// Close the account. Once closed, no further operations are allowed.
    /// </summary>
    public void Close()
    {
        EnsureOpen();
        IsClosed = true;
    }

    private void EnsureOpen()
    {
        if (IsClosed)
        { 
            throw new InvalidOperationException("Cannot operate on a closed account.");
        }
    }
}
