using System.ComponentModel.DataAnnotations.Schema;
using Centeva.DomainModeling.Interfaces;

namespace Centeva.DomainModeling;

/// <summary>
/// Base class for all entities that use the Id property to track identity
/// </summary>
/// <typeparam name="TId">Type of <see cref="Id"/> property</typeparam>
public abstract class BaseEntity<TId> : IEntityWithEvents
{
    public TId Id { get; set; } = default!;

    private readonly List<BaseDomainEvent> _domainEvents = new();

    [NotMapped]
    public IEnumerable<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void RegisterDomainEvent(BaseDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(BaseDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}

public abstract class BaseEntity : BaseEntity<int> { }