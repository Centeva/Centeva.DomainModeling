using System.ComponentModel.DataAnnotations.Schema;

namespace Centeva.DomainModeling;

/// <summary>
/// Base class for objects (typically entities) that have domain events
/// </summary>
/// <remarks>
/// This is used instead of an interface so that methods can be protected/internal
/// </remarks>
public abstract class ObjectWithEvents
{
    private readonly List<BaseDomainEvent> _domainEvents = new();

    [NotMapped]
    public IEnumerable<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(BaseDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    internal void ClearDomainEvents() => _domainEvents.Clear();
}