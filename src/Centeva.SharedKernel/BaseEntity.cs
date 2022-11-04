using System.ComponentModel.DataAnnotations.Schema;

namespace Centeva.SharedKernel;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }

    private readonly List<BaseDomainEvent> _domainEvents = new();

    [NotMapped] 
    public IEnumerable<BaseDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void RegisterDomainEvent(BaseDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(BaseDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

    internal void ClearDomainEvents() => _domainEvents.Clear();
}

public abstract class BaseEntity : BaseEntity<int> { }