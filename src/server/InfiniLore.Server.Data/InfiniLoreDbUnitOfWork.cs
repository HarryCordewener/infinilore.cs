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
    private readonly Lazy<Task<InfiniLoreDbContext>> _db = new(() => dbContextFactory.CreateDbContextAsync());
    private IDbContextTransaction? _transaction;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <inheritdoc/>
    public async Task CommitAsync(CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await GetDbContextAsync();
        if (_transaction == null) {
            await dbContext.SaveChangesAsync(ct);
            return;
        }

        await _transaction.CommitAsync(ct);
        _transaction = null;
    }

    /// <inheritdoc/>
    public async Task<bool> TryCommitAsync(CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await GetDbContextAsync();
        try {
            if (_transaction == null) {
                await dbContext.SaveChangesAsync(ct);
                return true;
            }

            await _transaction.CommitAsync(ct);
            _transaction = null;
            return true;
            
        } catch (Exception ex) {
            logger.Error(ex, "Error while committing transaction");
            return false;
        }
    }

    /// <inheritdoc/>
    public async Task BeginTransactionAsync(CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await GetDbContextAsync();
        _transaction = await dbContext.Database.BeginTransactionAsync(ct);
    }

    /// <inheritdoc/>
    public Task RollbackAsync(CancellationToken ct = default) => TryRollbackAsync(ct);

    /// <inheritdoc/>
    public async Task<bool> TryRollbackAsync(CancellationToken ct = default) {
        if (_transaction == null) return false;
        await _transaction.RollbackAsync(ct);
        _transaction = null;
        return true;
    }

    /// <inheritdoc/>
    public async Task<InfiniLoreDbContext> GetDbContextAsync() => await _db.Value;

    
    /// <summary>
    /// Asynchronously disposes the current resources.
    /// This method ensures the associated `InfiniLoreDbContext` and any
    /// active transactions are properly disposed asynchronously.
    /// </summary>
    public async ValueTask DisposeAsync() {
        if (_db.IsValueCreated) {
            InfiniLoreDbContext dbContext = await _db.Value;
            await dbContext.DisposeAsync();
        }

        if (_transaction is not null) {
            await _transaction.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }
}
