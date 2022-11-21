using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures.Entities;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures;

public abstract class IntegrationTestBase : IClassFixture<SharedDatabaseFixture>
{
    protected TestDbContext _dbContext;
    protected IConfigurationProvider _mappingConfigurationProvider;

    protected Repository<Person> _personRepository;
    protected Repository<Address> _addressRepository;

    protected IntegrationTestBase(SharedDatabaseFixture fixture)
    {
        _dbContext = fixture.CreateContext();
        _mappingConfigurationProvider = new MapperConfiguration(config => config.AddProfile<MappingProfile>());

        _personRepository = new Repository<Person>(_dbContext, SpecificationEvaluator.Default, _mappingConfigurationProvider);
        _addressRepository = new Repository<Address>(_dbContext, SpecificationEvaluator.Default, _mappingConfigurationProvider);
    }
}