namespace Centeva.DomainModeling.Interfaces;

public interface IEntityWithEvents
{
    public IEnumerable<BaseDomainEvent> DomainEvents { get; }

    public void RegisterDomainEvent(BaseDomainEvent domainEvent);

    public void RemoveDomainEvent(BaseDomainEvent domainEvent);

    public void ClearDomainEvents();
}
