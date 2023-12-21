using Ardalis.Specification;
using Centeva.DomainModeling.EFCore;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public class Repository<T> : BaseRepository<T> where T : class
{
    public Repository(TestDbContext dbContext, ISpecificationEvaluator specificationEvaluator) 
        : base(dbContext, specificationEvaluator)
    {
    }
}
