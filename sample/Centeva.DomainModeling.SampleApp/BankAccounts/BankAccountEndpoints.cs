using Microsoft.AspNetCore.Http.HttpResults;

namespace Centeva.DomainModeling.SampleApp.BankAccounts;

public static class BankAccountEndpoints
{
    public static void MapBankAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/accounts").WithName("BankAccounts");

        group.MapPost("/", OpenAccount)
            .WithName("OpenAccount")
            .WithDescription("Open a new bank account with an initial deposit.");

        group.MapGet("/{id:guid}", GetAccount)
            .WithName("GetAccount")
            .WithDescription("Get a bank account by id.");

        group.MapGet("/by-number/{accountNumber}", GetAccountByNumber)
            .WithName("GetAccountByNumber")
            .WithDescription("Get a bank account by its human-readable account number.");

        group.MapPost("/{id:guid}/deposit", Deposit)
            .WithName("Deposit")
            .WithDescription("Deposit funds into an account.");

        group.MapPost("/{id:guid}/withdraw", Withdraw)
            .WithName("Withdraw")
            .WithDescription("Withdraw funds from an account.");
    }

    private static async Task<Results<Created<BankAccountDto>, BadRequest<string>>> OpenAccount(
        OpenAccountRequest request,
        IRepository<BankAccount> repository)
    {
        try
        {
            var account = BankAccount.Open(
                new CustomerName(request.FirstName, request.LastName),
                new Money(request.InitialDeposit, request.Currency));

            await repository.AddAsync(account);

            return TypedResults.Created($"/accounts/{account.Id.Value}", ToDto(account));
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static async Task<Results<Ok<BankAccountDto>, NotFound>> GetAccount(
        Guid id,
        IReadRepository<BankAccount> repository)
    {
        var account = await repository.GetByIdAsync(new AccountId(id));
        return account is null ? TypedResults.NotFound() : TypedResults.Ok(ToDto(account));
    }

    private static async Task<Results<Ok<BankAccountDto>, NotFound>> GetAccountByNumber(
        string accountNumber,
        IReadRepository<BankAccount> repository)
    {
        var account = await repository.FirstOrDefaultAsync(new GetAccountByAccountNumberSpec(new AccountNumber(accountNumber)));
        return account is null ? TypedResults.NotFound() : TypedResults.Ok(ToDto(account));
    }

    private static async Task<Results<Ok<BankAccountDto>, NotFound, BadRequest<string>>> Deposit(
        Guid id,
        TransactionRequest request,
        IRepository<BankAccount> repository)
    {
        var account = await repository.GetByIdAsync(new AccountId(id));
        if (account is null)
        {
            return TypedResults.NotFound();
        }

        try
        {
            account.Deposit(new Money(request.Amount, request.Currency));
            await repository.UpdateAsync(account);
            return TypedResults.Ok(ToDto(account));
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static async Task<Results<Ok<BankAccountDto>, NotFound, BadRequest<string>>> Withdraw(
        Guid id,
        TransactionRequest request,
        IRepository<BankAccount> repository)
    {
        var account = await repository.GetByIdAsync(new AccountId(id));
        if (account is null)
        {
            return TypedResults.NotFound();
        }

        try
        {
            account.Withdraw(new Money(request.Amount, request.Currency));
            await repository.UpdateAsync(account);
            return TypedResults.Ok(ToDto(account));
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static BankAccountDto ToDto(BankAccount a) =>
        new(a.Id.Value, a.AccountNumber.Value, a.OwnerName.FullName, a.Balance.Amount, a.Balance.Currency, a.IsClosed);

    public record OpenAccountRequest(string FirstName, string LastName, decimal InitialDeposit, string Currency);
    public record TransactionRequest(decimal Amount, string Currency);
}
