using MediatR;

namespace Centeva.SharedKernel;

public class BaseDomainEvent : INotification
{
    public DateTimeOffset Occurred { get; protected set; } = DateTimeOffset.UtcNow;
}