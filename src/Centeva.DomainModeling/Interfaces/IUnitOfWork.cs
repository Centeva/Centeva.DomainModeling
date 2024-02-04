namespace Centeva.DomainModeling.Interfaces;

/// <summary>
/// Represents a Unit of Work for managing multiple repository transactions as a single unit.
/// </summary>
/// <remarks>
/// Any repository modifications that happen between <see cref="BeginTransaction"/> and <see cref="Commit"/> will be
/// committed together. If <see cref="Rollback"/> is called, all changes will be discarded. This is useful when
/// you are working with multiple repositories and want to ensure that all changes are committed together or not at all.
/// </remarks>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Begin a new transaction.  All changes will be rolled back if not committed.
    /// </summary>
    void BeginTransaction();
    
    /// <summary>
    /// Commit the transaction.  All changes will be saved.
    /// </summary>
    void Commit();
    
    /// <summary>
    /// Rollback the transaction.  All changes will be discarded.
    /// </summary>
    void Rollback();
    
    /// <summary>
    /// Save all pending changes and commit the transaction.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous save operation. The task result contains the
    /// number of state entries written.
    /// </returns>
    Task<int> SaveAndCommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Save all pending changes.  Does not commit the transaction.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous save operation. The task result contains the
    /// number of state entries written.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}