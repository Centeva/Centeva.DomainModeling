using Centeva.DomainModeling.Mediator;

namespace Centeva.DomainModeling.SampleApp.BankAccounts;

public class AccountOpenedEvent(AccountId accountId, CustomerName ownerName, Money initialDeposit)
    : BaseDomainEvent
{
    public AccountId AccountId { get; } = accountId;
    public CustomerName OwnerName { get; } = ownerName;
    public Money InitialDeposit { get; } = initialDeposit;
}
