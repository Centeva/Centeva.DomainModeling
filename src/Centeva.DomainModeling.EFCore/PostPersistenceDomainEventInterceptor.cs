using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Centeva.DomainModeling.EFCore;

internal class PostPersistenceDomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IDomainEventDispatcher _dispatcher;

    public PostPersistenceDomainEventInterceptor(IDomainEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        var entities = eventData.Context.GetEntitiesWithEvents();
        await _dispatcher.DispatchAndClearEvents(entities, cancellationToken);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
