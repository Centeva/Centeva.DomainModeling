using Ardalis.Specification;

namespace Centeva.DomainModeling.Interfaces;

/// <summary>
/// Defines additional repository methods that can project entities to a target type, likely using AutoMapper.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IProjectedReadRepository<T> : IReadRepository<T> where T : class
{
    /// <summary>
    /// Returns the first element of a sequence, projected to a target <typeparamref name="TResult" /> using an external tool like AutoMapper,
    /// or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="TResult" />, or <see langword="null"/>.
    /// </returns>
    Task<TResult?> FirstOrDefaultProjectedAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the first element of a sequence, projected to a target <typeparamref name="TResult" /> using an external tool like AutoMapper,
    /// or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="TResult" />, or <see langword="null"/>.
    /// </returns>
    Task<TResult?> SingleOrDefaultProjectedAsync<TResult>(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all entities of <typeparamref name="T" /> from the database.
    /// <para>
    /// Projects each entity into a new form using an external tool like AutoMapper, being <typeparamref name="TResult" />.
    /// </para>
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="IReadOnlyList{TResult}" /> that contains elements from the input sequence.
    /// </returns>
    Task<IReadOnlyList<TResult>> ListProjectedAsync<TResult>(CancellationToken cancellationToken = default);


    /// <summary>
    /// Finds all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    /// <paramref name="specification"/>, from the database.
    /// <para>
    /// Projects each entity into a new form using an external tool like AutoMapper, being <typeparamref name="TResult" />.
    /// </para>
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="IReadOnlyList{TResult}" /> that contains elements from the input sequence.
    /// </returns>
    Task<IReadOnlyList<TResult>> ListProjectedAsync<TResult>(ISpecification<T> specification, CancellationToken cancellationToken = default);
}

