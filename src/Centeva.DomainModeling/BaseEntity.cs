using System.ComponentModel.DataAnnotations.Schema;
using Centeva.DomainModeling.Interfaces;

namespace Centeva.DomainModeling;

/// <summary>
/// Base class for all entities including support for domain events that can be dispatched after persistence.
/// </summary>
/// <typeparam name="TId">Type of <see cref="Id"/> property, typically int or Guid</typeparam>
public abstract class BaseEntity<TId> : ObjectWithEvents
{
    public TId Id { get; set; } = default!;
}

/// <summary>
/// Base class for entities with an integer Id
/// </summary>
public abstract class BaseEntity : BaseEntity<int> { }
