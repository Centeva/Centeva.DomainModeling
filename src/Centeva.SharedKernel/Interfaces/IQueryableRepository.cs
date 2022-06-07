namespace Centeva.SharedKernel.Interfaces;

public interface IQueryableRepository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    /// <summary>
    /// Returns an <see cref="IQueryable{T}"/> for retrieving entities of type <typeparamref name="TEntity"/>
    /// </summary>
    /// <remarks>
    /// This exposes the internals of your storage mechanism or ORM, which could
    /// result in unexpected behavior, such as SQL queries being materialized
    /// late in the process.
    /// </remarks>
    /// <returns>
    /// The repository's contents as an <see cref="IQueryable{T}"/>
    /// </returns>
    [Obsolete("Use Specifications instead as this exposes unmaterialized queries")]
    IQueryable<TEntity> AsQueryable();
}
