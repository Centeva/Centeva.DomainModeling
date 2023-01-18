using MediatR;

namespace Centeva.SharedKernel;

/// <summary>
/// Used to communicate with other applications or bounded contexts, typically
/// via a message bus.  Can be created directly or in response to a domain event.
/// </summary>
public abstract class BaseIntegrationEvent : INotification
{
    public DateTimeOffset Occurred { get; protected set; } = DateTimeOffset.UtcNow;
    public abstract string EventType { get; }
}
