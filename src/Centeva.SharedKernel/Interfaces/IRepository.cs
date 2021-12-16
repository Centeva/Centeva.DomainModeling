namespace Centeva.SharedKernel.Interfaces;

public interface IRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IAggregateRoot
{
    /// <summary>
    /// Adds an entity in the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="saveChanges">Indicates whether changes should be persisted immediately.  You should use <see cref="SaveChangesAsync"/> at some point if this is false.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="T" />.
    /// </returns>
    Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity in the database
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="saveChanges">Indicates whether changes should be persisted immediately.  You should use <see cref="SaveChangesAsync"/> at some point if this is false.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes an entity in the database
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="saveChanges">Indicates whether changes should be persisted immediately.  You should use <see cref="SaveChangesAsync"/> at some point if this is false.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the given entities in the database
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    /// <param name="saveChanges">Indicates whether changes should be persisted immediately.  You should use <see cref="SaveChangesAsync"/> at some point if this is false.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Persists changes to the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the number of entries saved.
    /// </returns>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}