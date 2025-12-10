using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Centeva.DomainModeling.EFCore;

/// <summary>
/// Used to dispatch domain events after saving entities to the database
/// </summary>
/// <example>
/// Register this interceptor when you configure EF Core in Program.cs:
/// <code>
///    services.AddSingleton&lt;DispatchDomainEventsInterceptor&gt;();
///
///    services.AddDbContext&lt;ApplicationDbContext&gt;((sp, options) =&gt; options
///        .UseSqlServer(connectionString)
///        .AddInterceptors(
///            sp.GetRequiredService&lt;DispatchDomainEventsInterceptor&gt;()));
/// </code>
/// </example>
public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public DispatchDomainEventsInterceptor(IDomainEventDispatcher domainEventDispatcher)
    {
        _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
    }
    
    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData, 
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            await DispatchDomainEvents(eventData.Context, cancellationToken);
        }

        return result;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        // Synchronous dispatching is not supported - use SaveChangesAsync in your
        // application code to ensure domain events are dispatched.

        return result;
    }

    private async Task DispatchDomainEvents(DbContext context, CancellationToken cancellationToken)
    {
        var entitiesWithEvents = context.ChangeTracker
            .Entries<ObjectWithEvents>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        if (entitiesWithEvents.Count > 0)
        {
            await _domainEventDispatcher.DispatchAndClearEvents(entitiesWithEvents, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}