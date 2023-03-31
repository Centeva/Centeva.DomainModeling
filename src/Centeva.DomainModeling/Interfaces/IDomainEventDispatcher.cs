namespace Centeva.DomainModeling.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IEntityWithEvents> entitiesWithEvents, CancellationToken cancellationToken = default);
}
