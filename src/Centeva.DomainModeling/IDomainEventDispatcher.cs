namespace Centeva.DomainModeling;

/// <summary>
/// Interface for sending domain events.  Typically used to dispatch events after persistence using a tool like MediatR.
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<ObjectWithEvents> entitiesWithEvents, CancellationToken cancellationToken = default);
}
