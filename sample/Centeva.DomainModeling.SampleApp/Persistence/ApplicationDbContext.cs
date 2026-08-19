using Centeva.DomainModeling.SampleApp.BankAccounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Centeva.DomainModeling.SampleApp.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var accountIdConverter = new ValueConverter<AccountId, Guid>(
            id => id.Value,
            value => new AccountId(value));

        var accountNumberConverter = new ValueConverter<AccountNumber, string>(
            n => n.Value,
            value => new AccountNumber(value));

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).HasConversion(accountIdConverter);
            entity.Property(a => a.AccountNumber).HasConversion(accountNumberConverter).HasMaxLength(36);
            entity.HasIndex(a => a.AccountNumber).IsUnique();

            entity.ComplexProperty(a => a.OwnerName, n =>
            {
                n.Property(c => c.FirstName).HasMaxLength(100);
                n.Property(c => c.LastName).HasMaxLength(100);
            });

            entity.ComplexProperty(a => a.Balance, b =>
            {
                b.Property(m => m.Amount);
                b.Property(m => m.Currency).HasMaxLength(3);
            });
        });
    }
}
