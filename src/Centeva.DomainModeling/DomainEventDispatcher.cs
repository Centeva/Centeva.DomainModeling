using Centeva.DomainModeling.Interfaces;
using MediatR;

namespace Centeva.DomainModeling;

/// <summary>
/// Used to dispatch domain events, usually as part of the data persistence process.
/// </summary>
/// <example>
/// Run as part of your Entity Framework Core save process:
/// <code>
///    public override async Task&lt;int&gt; SaveChangesAsync(CancellationToken cancellationToken = default)
///    {
///        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
///
///        await _domainEventDispatcher.DispatchAndClearEvents(this.GetEntitiesWithEvents(), cancellationToken).ConfigureAwait(false);
///
///        return result;
///    }
/// </code>
/// </example>
public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IPublisher _publisher;

    public DomainEventDispatcher(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task DispatchAndClearEvents(IEnumerable<IEntityWithEvents> entitiesWithEvents, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                await _publisher.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
