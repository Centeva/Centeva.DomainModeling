namespace Centeva.SharedKernel;

public abstract class BaseEntity<TId>
{
    public TId Id { get; private set; } = default!;

    public List<BaseDomainEvent> Events { get; protected set; } = new();
}

public abstract class BaseEntity : BaseEntity<int> { }