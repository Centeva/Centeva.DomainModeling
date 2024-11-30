using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Centeva.DomainModeling.EFCore;

public class PostSaveEventDispatchingInterceptor : SaveChangesInterceptor
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public PostSaveEventDispatchingInterceptor(IDomainEventDispatcher domainEventDispatcher)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }
    
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = default)
    {
        var entitiesWithEvents = eventData.Context?.GetEntitiesWithEvents() ?? [];
        await _domainEventDispatcher.DispatchAndClearEvents(entitiesWithEvents, cancellationToken);
        return result;
    }
}