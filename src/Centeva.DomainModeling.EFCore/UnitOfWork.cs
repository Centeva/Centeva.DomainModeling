using Centeva.DomainModeling.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Centeva.DomainModeling.EFCore;

/// <summary>
/// Unit of Work implementation for Entity Framework Core.  Register this with your DI container
/// as an implementation of <see cref="IUnitOfWork"/>.  If using Microsoft.Extensions.DependencyInjection,
/// use AddScoped to ensure that the same instance is used throughout a single request.  You can also
/// use the provided <see cref="ServiceCollectionExtensions.AddUnitOfWork"/> extension method.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Dispose()
    {
        if (_transaction == null)
        {
            return;
        }
        
        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public void BeginTransaction()
    {
        if (_transaction != null)
        {
            return;
        }

        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void Commit()
    {
        if (_transaction == null)
        {
            return;
        }

        _transaction.Commit();
        _transaction.Dispose();
        _transaction = null;
    }

    public void Rollback()
    {
        if (_transaction == null)
        {
            return;
        }

        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }

    public async Task<int> SaveAndCommitAsync(CancellationToken cancellationToken = default)
    {
        var result = await SaveChangesAsync(cancellationToken);
        Commit();
        return result;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}