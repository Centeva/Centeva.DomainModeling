using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.EFCore;

public static class DbContextExtensions
{
    /// <summary>
    /// Get tracked entities that have domain events
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    public static List<ObjectWithEvents> GetEntitiesWithEvents(this DbContext dbContext) =>
        dbContext.ChangeTracker
            .Entries<ObjectWithEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();
}
