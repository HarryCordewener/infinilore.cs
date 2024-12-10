// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.DependencyInjection;
using InfiniLore.Server.Contracts.Database;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace InfiniLore.Database.MsSqlServer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

/// <inheritdoc cref="IDbUnitOfWork{T}" />
[InjectableService<IDbUnitOfWork<MsSqlDbContext>>(ServiceLifetime.Scoped)]
public class MsSqlDbUnitOfWork(IDbContextFactory<MsSqlDbContext> dbContextFactory, ILogger logger) : IDbUnitOfWork<MsSqlDbContext> {
    private MsSqlDbContext? _msSqlDb;
    private IDbContextTransaction? _transaction;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <inheritdoc />
    public async Task CommitAsync(CancellationToken ct = default) {
        MsSqlDbContext dbContext = await GetDbContextAsync(ct);
        if (_transaction == null) {
            await dbContext.SaveChangesAsync(ct);
            return;
        }

        await _transaction.CommitAsync(ct);
        _transaction = null;
    }

    /// <inheritdoc />
    public async Task<bool> TryCommitAsync(CancellationToken ct = default) {
        MsSqlDbContext dbContext = await GetDbContextAsync(ct);
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
        MsSqlDbContext dbContext = await GetDbContextAsync(ct);
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
    public async Task<MsSqlDbContext> GetDbContextAsync(CancellationToken ct = default) =>
        _msSqlDb ??= await dbContextFactory.CreateDbContextAsync(ct);

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
        if (_msSqlDb != null) await _msSqlDb.DisposeAsync();
        if (_transaction != null) await _transaction.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    public void Dispose() {
        _msSqlDb?.Dispose();
        _transaction?.Dispose();
        GC.SuppressFinalize(this);
    }
}
