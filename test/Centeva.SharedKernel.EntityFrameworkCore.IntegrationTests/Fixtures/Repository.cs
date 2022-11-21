using Ardalis.Specification;
using AutoMapper;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures;

public class Repository<T> : BaseRepository<T> where T : class
{
    protected readonly TestDbContext _dbContext;

    public Repository(TestDbContext dbContext, ISpecificationEvaluator specificationEvaluator, IConfigurationProvider configurationProvider) 
        : base(dbContext, configurationProvider, specificationEvaluator)
    {
        _dbContext = dbContext;
    }
}
