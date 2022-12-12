using Ardalis.Specification;
using AutoMapper;
using Centeva.SharedKernel.EFCore.AutoMapper;

namespace Centeva.SharedKernel.IntegrationTests.Fixtures;

public class Repository<T> : BaseProjectedRepository<T> where T : class
{
    protected readonly TestDbContext _dbContext;

    public Repository(TestDbContext dbContext, ISpecificationEvaluator specificationEvaluator, IConfigurationProvider configurationProvider) 
        : base(dbContext, configurationProvider, specificationEvaluator)
    {
        _dbContext = dbContext;
    }
}
