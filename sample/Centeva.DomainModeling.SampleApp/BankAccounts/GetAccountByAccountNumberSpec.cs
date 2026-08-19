using Ardalis.Specification;

namespace Centeva.DomainModeling.SampleApp.BankAccounts;

/// <summary>
/// Finds a <see cref="BankAccount"/> by its human-readable account number.
/// Demonstrates a Specification that queries by a non-primary-key unique field,
/// which cannot be satisfied by <see cref="IReadRepository{T}.GetByIdAsync"/>.
/// </summary>
public class GetAccountByAccountNumberSpec : SingleResultSpecification<BankAccount>
{
    public GetAccountByAccountNumberSpec(AccountNumber accountNumber) =>
        Query.Where(a => a.AccountNumber == accountNumber);
}
