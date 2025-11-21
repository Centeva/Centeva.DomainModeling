using MediatR;

namespace Centeva.DomainModeling.MediatR;

/// <summary>
/// Used to trigger side effects within an application or bounded context, usually
/// as part of the same transaction.  Typically used to make changes in other
/// aggregates.
/// </summary>
/// <see cref="IDomainEventDispatcher"/>
public abstract class BaseDomainEvent : INotification, IDomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
