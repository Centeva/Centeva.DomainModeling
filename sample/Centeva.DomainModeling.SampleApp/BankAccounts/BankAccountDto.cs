namespace Centeva.DomainModeling.SampleApp.BankAccounts;

public record BankAccountDto(Guid Id, string AccountNumber, string OwnerName, decimal Balance, string Currency, bool IsClosed);
