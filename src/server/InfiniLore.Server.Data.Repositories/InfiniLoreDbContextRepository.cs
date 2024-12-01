// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace InfiniLore.Server.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class InfiniLoreDbContextRepository<T>(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : IRepository<InfiniLoreDbContext> where T : class {
    #region Queryables
    public async ValueTask<InfiniLoreDbContext> GetDbContextAsync(CancellationToken ct = default) => await unitOfWork.GetDbContextAsync(ct);

    protected async ValueTask<DbSet<T>> GetDbSetAsync(CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await GetDbContextAsync(ct);
        return dbContext.Set<T>();
    }
    #endregion
}
