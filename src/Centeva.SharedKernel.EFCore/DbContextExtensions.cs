using Centeva.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Centeva.SharedKernel.EFCore;

public static class DbContextExtensions
{
    /// <summary>
    /// Get tracked entities that have domain events
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    public static List<IEntityWithEvents> GetEntitiesWithEvents(this DbContext dbContext) =>
        dbContext.ChangeTracker
            .Entries()
            .Select(e => e.Entity as IEntityWithEvents)
            .Where(e => e is not null && e.DomainEvents.Any())
            .Select(e => e!)
            .ToList();
}
