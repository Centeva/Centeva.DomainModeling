
using Mediator;

namespace Centeva.DomainModeling.Mediator;

/// <summary>
/// Used to dispatch domain events, usually as part of the data persistence process.
/// </summary>
public class MediatorDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IPublisher _publisher;

    public MediatorDomainEventDispatcher(IPublisher publisher)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public async Task DispatchAndClearEvents(IEnumerable<ObjectWithEvents> entitiesWithEvents, CancellationToken cancellationToken = default)
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
