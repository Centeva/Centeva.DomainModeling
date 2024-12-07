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
        _domainEventDispatcher = domainEventDispatcher;
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

    private async Task DispatchDomainEvents(DbContext context, CancellationToken cancellationToken)
    {
        var entitiesWithEvents = context.ChangeTracker
            .Entries<ObjectWithEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        await _domainEventDispatcher.DispatchAndClearEvents(entitiesWithEvents, cancellationToken)
            .ConfigureAwait(false);
    }
}