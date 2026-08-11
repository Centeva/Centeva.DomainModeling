namespace Centeva.DomainModeling;

/// <summary>
/// Implemented by objects (typically aggregate roots) that raise domain events.
/// </summary>
/// <remarks>
/// You can implement this interface directly when you want to raise domain events from an
/// object that is not an entity or aggregate root, but this is not common.
/// </remarks>
public interface IHasDomainEvents
{
    /// <summary>
    /// The domain events currently registered on this object, in the order they were raised.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Clears all currently registered domain events.  Typically called by a dispatcher
    /// after the events have been published.
    /// </summary>
    void ClearDomainEvents();
}
