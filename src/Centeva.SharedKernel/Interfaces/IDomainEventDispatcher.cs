namespace Centeva.SharedKernel.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IEntityWithEvents> entitiesWithEvents);
}
