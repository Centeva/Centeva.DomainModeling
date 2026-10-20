using Centeva.DomainModeling.UnitTests.Fixtures.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public abstract class IntegrationTestBase : IClassFixture<SharedDatabaseFixture>, IDisposable
{
    protected TestDbContext _dbContext;

    protected Repository<Person> _personRepository;
    protected readonly IDomainEventDispatcher _dispatcher;

    // All tests in a class share one in-memory database, so each test runs in a
    // transaction that is rolled back afterward to keep the seed data intact.
    private readonly IDbContextTransaction _transaction;

    protected IntegrationTestBase(SharedDatabaseFixture fixture)
    {
        _dbContext = fixture.CreateContext();
        _dispatcher = fixture.DomainEventDispatcher;
        _transaction = _dbContext.Database.BeginTransaction();

        _personRepository = new Repository<Person>(_dbContext);
    }

    public void Dispose()
    {
        _transaction.Rollback();
        _transaction.Dispose();
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
