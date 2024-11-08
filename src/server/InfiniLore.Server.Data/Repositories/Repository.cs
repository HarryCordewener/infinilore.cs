// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;

namespace InfiniLore.Server.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class Repository<T>(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) where T : class {
    #region Queryables
    internal async Task<DbSet<T>> GetDbSetAsync() {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync();
        return dbContext.Set<T>();
    }
    #endregion
}
