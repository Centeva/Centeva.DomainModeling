using Centeva.DomainModeling.EFCore;

namespace Centeva.DomainModeling.SampleApp.Persistence;

public class EfRepository<T> : BaseRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }
}
