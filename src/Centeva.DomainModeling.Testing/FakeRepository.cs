using System.Linq.Expressions;
using Ardalis.Specification;
using Centeva.DomainModeling.Interfaces;

namespace Centeva.DomainModeling.Testing;

/// <summary>
/// In-memory Repository implementation for use in unit tests
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class FakeRepository<TEntity> : FakeRepository<TEntity, int> where TEntity : BaseEntity
{
}

/// <summary>
/// In-memory Repository implementation for use in unit tests
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public class FakeRepository<TEntity, TKey> : IBaseRepository<TEntity>
    where TEntity : BaseEntity<TKey>
    where TKey : notnull
{
    private readonly List<TEntity> _entities;

    public FakeRepository()
    {
        _entities = new List<TEntity>();
    }

    public FakeRepository(List<TEntity> entities)
    {
        _entities = entities;
    }

    public Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_entities).Any());
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.Any());
    }

    public Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_entities).Count());
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_entities.Count);
    }

    public Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_entities).FirstOrDefault());
    }

    public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_entities).FirstOrDefault());
    }

    public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<TEntity> specification,
        Expression<Func<TEntity, TResult>> projection,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_entities).Select(projection.Compile()).FirstOrDefault());
    }
    
    public Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return Task.FromResult(_entities.Find(x => x.Id.Equals(id)));
    }

    public Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<TEntity>>(_entities);
    }

    public Task<IReadOnlyList<TResult>> ListAsync<TResult>(Expression<Func<TEntity, TResult>> projection,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<TResult>>(_entities.Select(projection.Compile()).ToList());
    }

    public Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<TEntity>>(specification.Evaluate(_entities).ToList());
    }

    public Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<TEntity> specification,
        Expression<Func<TEntity, TResult>> projection,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<TResult>>(specification.Evaluate(_entities).Select(projection.Compile()).ToList());
    }

    public Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<TResult>>(specification.Evaluate(_entities).ToList());
    }

    public Task<TEntity?> SingleOrDefaultAsync(ISingleResultSpecification<TEntity> specification,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_entities).SingleOrDefault());
    }

    public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<TEntity, TResult> specification,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_entities).SingleOrDefault());
    }

    public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<TEntity> specification,
        Expression<Func<TEntity, TResult>> projection,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(specification.Evaluate(_entities).Select(projection.Compile()).SingleOrDefault());
    }

    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _entities.Add(entity);

        return Task.FromResult(entity);
    }

    public Task<IReadOnlyList<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        IReadOnlyList<TEntity> entitiesList = entities.ToList();
        _entities.AddRange(entitiesList);

        return Task.FromResult(entitiesList);
    }

    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var existingEntry = _entities.FirstOrDefault(x => x.Id.Equals(entity.Id));

        if (existingEntry != null)
        {
            _entities.Remove(existingEntry);
        }

        _entities.Add(entity);

        return Task.CompletedTask;
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await UpdateAsync(entity, cancellationToken);
        }
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _entities.Remove(entity);

        return Task.CompletedTask;
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            await DeleteAsync(entity, cancellationToken);
        }
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(0);
    }
}