using Ardalis.Specification.EntityFrameworkCore;
using Centeva.DomainModeling.UnitTests.Fixtures.Entities;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public abstract class IntegrationTestBase : IClassFixture<SharedDatabaseFixture>
{
    protected readonly TestDbContext _dbContext;

    protected readonly Repository<Person> _personRepository;
    protected readonly Repository<Address> _addressRepository;

    protected IntegrationTestBase(SharedDatabaseFixture fixture)
    {
        _dbContext = fixture.CreateContext();

        _personRepository = new Repository<Person>(_dbContext, SpecificationEvaluator.Default);
        _addressRepository = new Repository<Address>(_dbContext, SpecificationEvaluator.Default);
    }
}