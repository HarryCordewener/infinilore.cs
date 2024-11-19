// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;

namespace InfiniLore.Server.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <inheritdoc cref="InfiniLore.Server.Contracts.Data.IDbUnitOfWork{T}" />
[RegisterService<IDbUnitOfWork<InfiniLoreDbContext>>(LifeTime.Scoped)]
public class InfiniLoreDbUnitOfWork(IDbContextFactory<InfiniLoreDbContext> dbContextFactory, ILogger logger) : IDbUnitOfWork<InfiniLoreDbContext> {
    private InfiniLoreDbContext? _db;
    private IDbContextTransaction? _transaction;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <inheritdoc />
    public async Task CommitAsync(CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await GetDbContextAsync(ct);
        if (_transaction == null) {
            await dbContext.SaveChangesAsync(ct);
            return;
        }

        await _transaction.CommitAsync(ct);
        _transaction = null;
    }

    /// <inheritdoc />
    public async Task<bool> TryCommitAsync(CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await GetDbContextAsync(ct);
        try {
            if (_transaction == null) {
                await dbContext.SaveChangesAsync(ct);
                return true;
            }

            await _transaction.CommitAsync(ct);
            _transaction = null;
            return true;

        }
        catch (Exception ex) {
            logger.Error(ex, "Error while committing transaction");
            return false;
        }
    }

    /// <inheritdoc />
    public async Task BeginTransactionAsync(CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await GetDbContextAsync(ct);
        _transaction = await dbContext.Database.BeginTransactionAsync(ct);
    }

    /// <inheritdoc />
    public Task RollbackAsync(CancellationToken ct = default) => TryRollbackAsync(ct);

    /// <inheritdoc />
    public async Task<bool> TryRollbackAsync(CancellationToken ct = default) {
        if (_transaction == null) return false;

        await _transaction.RollbackAsync(ct);
        _transaction = null;
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> TryRollbackToSavepointAsync(string name, CancellationToken ct = default) {
        if (_transaction == null) return false;

        await _transaction.RollbackToSavepointAsync(name, ct);
        return true;
    }

    /// <inheritdoc />
    public async Task<InfiniLoreDbContext> GetDbContextAsync(CancellationToken ct = default) =>
        _db ??= await dbContextFactory.CreateDbContextAsync(ct);

    /// <inheritdoc />
    public async Task CreateSavepointAsync(string name, CancellationToken ct = default) {
        await (_transaction?.CreateSavepointAsync(name, ct) ?? Task.CompletedTask);
    }

    /// <summary>
    ///     Asynchronously disposes the current resources.
    ///     This method ensures the associated `InfiniLoreDbContext` and any
    ///     active transactions are properly disposed asynchronously.
    /// </summary>
    public async ValueTask DisposeAsync() {
        if (_db != null) await _db.DisposeAsync();
        if (_transaction != null) await _transaction.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    public void Dispose() {
        _db?.Dispose();
        _transaction?.Dispose();
        GC.SuppressFinalize(this);
    }
}
