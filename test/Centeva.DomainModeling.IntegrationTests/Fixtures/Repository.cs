using Centeva.DomainModeling.EFCore;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public class Repository<T> : BaseRepository<T> where T : class, IAggregateRoot
{
    public Repository(TestDbContext dbContext)
        : base(dbContext)
    {
    }
}
