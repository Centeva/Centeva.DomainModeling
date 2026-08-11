using System.ComponentModel.DataAnnotations.Schema;

namespace Centeva.DomainModeling;

/// <summary>
/// Base class for objects (typically aggregate roots) that raise domain events.
/// </summary>
public abstract class ObjectWithEvents : IHasDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Registers a domain event to be dispatched at persistence time.
    /// </summary>
    /// <remarks>
    /// Domain events should only be raised from aggregate roots (types implementing
    /// <see cref="IAggregateRoot"/>).  Child entities should mutate state via methods
    /// on their root, which then raises the event, so that the aggregate remains the
    /// consistency boundary for its invariants.
    /// </remarks>
    /// <param name="domainEvent">The domain event to register.</param>
    protected void RegisterDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
