using Centeva.DomainModeling.Mediator;

namespace Centeva.DomainModeling.SampleApp.BankAccounts;

public class FundsWithdrawnEvent(AccountId accountId, Money amount) : BaseDomainEvent
{
    public AccountId AccountId { get; } = accountId;
    public Money Amount { get; } = amount;
}
