// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;

namespace InfiniLore.Server.Contracts.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Implementation of the unit of work pattern specific to InfiniLore database context.
/// </summary>
public interface IDbUnitOfWork<T> : IAsyncDisposable, IDisposable where T : DbContext {
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // ----------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// When a transaction is started, will commit the transaction, else will save directly to the database.
    /// </summary>
    /// <param name="ct">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task CommitAsync(CancellationToken ct = default);

    /// <summary>
    /// Attempts to save all changes made in this context to the database. 
    /// </summary>
    /// <param name="ct">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the commit was successful.</returns>
    Task<bool> TryCommitAsync(CancellationToken ct = default);

    /// <summary>
    /// Begins a new database transaction asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task BeginTransactionAsync(CancellationToken ct = default);

    /// <summary>
    /// Asynchronously rolls back all changes made in the current transaction.
    /// Silently fails if no transactions has been started yet.
    /// </summary>
    /// <param name="ct">A CancellationToken to observe while waiting for the task to complete.</param>
    Task RollbackAsync(CancellationToken ct = default);

    /// <summary>
    /// Attempts to rollback the current database transaction. If no transaction is active, returns false.
    /// </summary>
    /// <returns>
    /// Returns a boolean indicating whether the rollback was successful.
    /// Returns true if the rollback occurred, otherwise returns false.
    /// </returns>
    Task<bool> TryRollbackAsync(CancellationToken ct = default);

    /// <summary>
    /// Asynchronously attempts to roll back the transaction to a specified savepoint.
    /// </summary>
    /// <param name="name">The name of the savepoint to roll back to.</param>
    /// <param name="ct">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous rollback operation. The task result contains a boolean value indicating whether the rollback to the savepoint was successful.</returns>
    Task<bool> TryRollbackToSavepointAsync(string name, CancellationToken ct = default);

    /// <summary>
    /// Asynchronously creates a savepoint in the current database transaction.
    /// </summary>
    /// <param name="name">The name to assign to the savepoint.</param>
    /// <param name="ct">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CreateSavepointAsync(string name, CancellationToken ct = default);
    
    /// <summary>
    /// Asynchronously retrieves the current InfiniLoreDbContext instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the InfiniLoreDbContext instance.</returns>
    /// <remarks>Not using a getter property to ensure proper usage of CancellationToken</remarks>
    Task<T> GetDbContextAsync(CancellationToken ct = default);
    
    
}
