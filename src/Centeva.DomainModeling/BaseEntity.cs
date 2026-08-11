namespace Centeva.DomainModeling;

/// <summary>
/// Base class for all entities including support for domain events that can be dispatched after persistence.
/// </summary>
/// <typeparam name="TId">
/// Type of <see cref="Id"/> property, typically <see cref="Guid"/>.  Must be a non-nullable value type
/// that implements <see cref="IEquatable{T}"/>, which covers all common choices (<see cref="int"/>,
/// <see cref="Guid"/>, custom <c>readonly record struct</c> strongly-typed IDs, etc.).
/// </typeparam>
public abstract class BaseEntity<TId> : ObjectWithEvents
    where TId : struct, IEquatable<TId>
{
    public TId Id { get; init; }
}
