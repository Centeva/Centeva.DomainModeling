namespace Centeva.DomainModeling;

/// <summary>
/// Marker contract for domain events raised by aggregate roots.
/// </summary>
/// <remarks>
/// A domain event represents something meaningful that happened within the domain.
/// Implement this interface for each event type and publish it after the aggregate's
/// state has been persisted.
/// <para>
/// Consumers that need additional metadata (such as a unique event ID or correlation ID)
/// should extend this interface or add properties to their concrete event types.
/// </para>
/// </remarks>
public interface IDomainEvent
{
    /// <summary>
    /// The UTC date and time at which the event occurred.
    /// </summary>
    DateTime DateOccurred { get; }
}
