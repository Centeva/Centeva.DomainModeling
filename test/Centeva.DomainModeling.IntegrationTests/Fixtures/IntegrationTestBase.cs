using Ardalis.Specification.EntityFrameworkCore;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public abstract class IntegrationTestBase : IClassFixture<SharedDatabaseFixture>
{
    protected TestDbContext _dbContext;

    protected Repository<Person> _personRepository;
    protected readonly IDomainEventDispatcher _dispatcher;

    protected IntegrationTestBase(SharedDatabaseFixture fixture)
    {
        _dbContext = fixture.CreateContext();
        _dispatcher = fixture.DomainEventDispatcher;

        _personRepository = new Repository<Person>(_dbContext, SpecificationEvaluator.Default);
    }
}
