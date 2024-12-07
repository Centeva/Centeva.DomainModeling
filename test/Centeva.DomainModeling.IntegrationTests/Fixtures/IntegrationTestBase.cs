using Ardalis.Specification.EntityFrameworkCore;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public abstract class IntegrationTestBase : IClassFixture<SharedDatabaseFixture>
{
    protected TestDbContext _dbContext;

    protected Repository<Person> _personRepository;
    protected Repository<Address> _addressRepository;
    protected readonly IDomainEventDispatcher _dispatcher;

    protected IntegrationTestBase(SharedDatabaseFixture fixture)
    {
        _dbContext = fixture.CreateContext();
        _dispatcher = fixture.DomainEventDispatcher;

        _personRepository = new Repository<Person>(_dbContext, SpecificationEvaluator.Default);
        _addressRepository = new Repository<Address>(_dbContext, SpecificationEvaluator.Default);
    }
}