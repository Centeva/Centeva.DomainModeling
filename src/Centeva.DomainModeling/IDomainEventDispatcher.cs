namespace Centeva.DomainModeling;

/// <summary>
/// Interface for sending domain events.  Typically used to dispatch events after persistence using a tool like MediatR.
/// </summary>
/// <remarks>
/// Implementations only publish the events they are given.  Collecting events from entities and
/// clearing them (see <see cref="IHasDomainEvents"/>) is the responsibility of the caller, such as
/// an EF Core interceptor.
/// </remarks>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Publishes each of the given domain events, in order.
    /// </summary>
    /// <param name="domainEvents">The domain events to publish.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}
