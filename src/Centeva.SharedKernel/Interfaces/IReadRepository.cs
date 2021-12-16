using Ardalis.Specification;

namespace Centeva.SharedKernel.Interfaces;

public interface IReadRepository<TEntity> where TEntity : class, IAggregateRoot
{
    /// <summary>
    /// Finds an entity with the given primary key value.
    /// </summary>
    /// <typeparam name="TId">The type of primary key.</typeparam>
    /// <param name="id">The value of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="T" />, or <see langword="null"/>.
    /// </returns>
    Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

    /// <summary>
    /// Finds an entity that matches the encapsulated query logic of the <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="T" />, or <see langword="null"/>.
    /// </returns>
    Task<TEntity?> GetBySpecAsync<TSpec>(TSpec specification, CancellationToken cancellationToken = default)
        where TSpec : ISingleResultSpecification, ISpecification<TEntity>;

    /// <summary>
    /// Finds an entity that matches the encapsulated query logic of the <paramref name="specification"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="TResult" />.
    /// </returns>
    Task<TResult> GetBySpecAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all entities of <typeparamref name="T" /> from the database.
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    /// <paramref name="specification"/>, from the database.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    /// <paramref name="specification"/>, from the database.
    /// <para>
    /// Projects each entity into a new form, being <typeparamref name="TResult" />.
    /// </para>
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="List{TResult}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a number that represents how many entities satisfy the encapsulated query logic
    /// of the <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the
    /// number of elements in the input sequence.
    /// </returns>
    Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the total number of records.
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the
    /// number of elements in the input sequence.
    /// </returns>
    Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a boolean whether any entity exists.
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if
    /// there are any entities, otherwise false.
    /// </returns>
    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a boolean whether any entities satisfy the encapsulated query logic
    /// of the <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if
    /// any entities satisfy the specification, otherwise false.
    /// </returns>
    Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns an <see cref="IQueryable{T}"/> for retrieving entities of type <typeparamref name="TEntity"/>
    /// </summary>
    /// <remarks>
    /// This exposes the internals of your storage mechanism or ORM, which could
    /// result in unexpected behavior, such as SQL queries being materialized
    /// late in the process.
    /// </remarks>
    /// <returns></returns>
    [Obsolete("Use Specifications instead as this exposes unmaterialized queries")]
    IQueryable<TEntity> Query();
}

