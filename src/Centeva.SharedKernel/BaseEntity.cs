namespace Centeva.SharedKernel;

public abstract class BaseEntity<TId>
{
    public TId Id { get; private set; } = default!;

    public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
}

public abstract class BaseEntity : BaseEntity<int> { }