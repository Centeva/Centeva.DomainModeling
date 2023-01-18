using Centeva.SharedKernel.Interfaces;
using MediatR;

namespace Centeva.SharedKernel;

/// <summary>
/// Used to trigger side effects within an application or bounded context, usually
/// as part of the same transaction.  Typically used to make changes in other
/// aggregates.
/// </summary>
/// <see cref="IDomainEventDispatcher"/>
public abstract class BaseDomainEvent : INotification
{
    public DateTimeOffset Occurred { get; protected set; } = DateTimeOffset.UtcNow;
}
