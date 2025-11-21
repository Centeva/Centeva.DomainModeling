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
    private readonly List<IDomainEvent> _domainEvents = [];

    [NotMapped]
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    internal void ClearDomainEvents() => _domainEvents.Clear();
}