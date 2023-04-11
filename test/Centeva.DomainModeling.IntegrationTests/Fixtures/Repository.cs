using Ardalis.Specification;
using AutoMapper;
using Centeva.DomainModeling.EFCore.AutoMapper;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public class Repository<T> : BaseProjectedRepository<T> where T : class
{
    protected readonly TestDbContext _dbContext;

    public Repository(TestDbContext dbContext, ISpecificationEvaluator specificationEvaluator, IConfigurationProvider configurationProvider) 
        : base(dbContext, configurationProvider, specificationEvaluator)
    {
        _dbContext = dbContext;
    }
}
