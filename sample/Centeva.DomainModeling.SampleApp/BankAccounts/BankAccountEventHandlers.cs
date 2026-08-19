using Mediator;

namespace Centeva.DomainModeling.SampleApp.BankAccounts;

/// <summary>
/// Handles domain events raised by <see cref="BankAccount"/>.
/// In a real application these handlers might send notifications, update
/// read models, or trigger downstream workflows.
/// </summary>
public class BankAccountEventHandlers(ILogger<BankAccountEventHandlers> logger)
    : INotificationHandler<AccountOpenedEvent>,
      INotificationHandler<FundsDepositedEvent>,
      INotificationHandler<FundsWithdrawnEvent>
{
    public ValueTask Handle(AccountOpenedEvent e, CancellationToken ct)
    {
        logger.LogInformation(
            "Account opened: {AccountId} for {OwnerName} with initial deposit of {Deposit}.",
            e.AccountId, e.OwnerName, e.InitialDeposit);
        return ValueTask.CompletedTask;
    }

    public ValueTask Handle(FundsDepositedEvent e, CancellationToken ct)
    {
        logger.LogInformation(
            "Funds deposited: {Amount} to account {AccountId}.",
            e.Amount, e.AccountId);
        return ValueTask.CompletedTask;
    }

    public ValueTask Handle(FundsWithdrawnEvent e, CancellationToken ct)
    {
        logger.LogInformation(
            "Funds withdrawn: {Amount} from account {AccountId}.",
            e.Amount, e.AccountId);
        return ValueTask.CompletedTask;
    }
}
