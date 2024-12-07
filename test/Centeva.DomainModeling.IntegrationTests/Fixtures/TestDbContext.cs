using Centeva.DomainModeling.EFCore;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Centeva.DomainModeling.UnitTests.Fixtures.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public class TestDbContext : DbContext
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    public DbSet<Person> People => Set<Person>();
    public DbSet<Address> Addresses => Set<Address>();

    public TestDbContext(DbContextOptions options, IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new DispatchDomainEventsInterceptor(_domainEventDispatcher));
        // Configure logging so that test output contains SQL statements, etc.
        optionsBuilder.UseLoggerFactory(LoggerFactoryProvider.LoggerFactoryInstance);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>().HasMany(x => x.Addresses).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);

        modelBuilder.Entity<Person>().HasData(PersonSeed.Get());
        modelBuilder.Entity<Address>().HasData(AddressSeed.Get());
    }
}
