using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.EFCore;

public static class DbContextExtensions
{
    /// <summary>
    /// Gathers the domain events from all tracked <see cref="IHasDomainEvents"/> entities and
    /// clears them from those entities.
    /// </summary>
    /// <remarks>
    /// Every entity is cleared before the events are returned, so a handler that triggers another
    /// <c>SaveChanges</c> will not cause the same events to be collected again.
    /// </remarks>
    /// <returns>The collected domain events, in the order they were raised on each entity.</returns>
    public static IReadOnlyList<IDomainEvent> CollectAndClearDomainEvents(this DbContext context)
    {
        var entitiesWithEvents = context.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Where(e => e.Entity.DomainEvents.Count > 0)
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entitiesWithEvents
            .SelectMany(e => e.DomainEvents)
            .ToList();

        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }

        return domainEvents;
    }
}
