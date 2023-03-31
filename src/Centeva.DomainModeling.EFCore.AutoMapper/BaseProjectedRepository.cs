using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Centeva.DomainModeling.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Centeva.DomainModeling.EFCore.AutoMapper;

/// <summary>
/// Repository implementation for Entity Framework Core, using AutoMapper for external projections.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseProjectedRepository<T> : BaseRepository<T>, IProjectedReadRepository<T> where T : class
{
    private readonly DbContext _dbContext;
    private readonly IConfigurationProvider _mappingConfigurationProvider;

    protected BaseProjectedRepository(DbContext dbContext, IConfigurationProvider mappingConfigurationProvider)
        : this(dbContext, mappingConfigurationProvider, SpecificationEvaluator.Default)
    {
    }

    protected BaseProjectedRepository(DbContext dbContext, IConfigurationProvider mappingConfigurationProvider,
        ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
    {
        _dbContext = dbContext;
        _mappingConfigurationProvider = mappingConfigurationProvider;
    }

    /// <inheritdoc/>
    public virtual async Task<TResult?> FirstOrDefaultProjectedAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).AsNoTracking().ProjectTo<TResult>(_mappingConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResult?> SingleOrDefaultProjectedAsync<TResult>(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).AsNoTracking().ProjectTo<TResult>(_mappingConfigurationProvider)
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TResult>> ListProjectedAsync<TResult>(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AsNoTracking().ProjectTo<TResult>(_mappingConfigurationProvider).ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<TResult>> ListProjectedAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).AsNoTracking().ProjectTo<TResult>(_mappingConfigurationProvider).ToListAsync(cancellationToken);
    }
}