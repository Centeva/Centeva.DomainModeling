namespace Centeva.SharedKernel;

public abstract class BaseEntity<TId>
{
    public virtual TId Id { get; set; } = default!;

    public List<BaseDomainEvent> Events = new();
}

public abstract class BaseEntity : BaseEntity<int> { }