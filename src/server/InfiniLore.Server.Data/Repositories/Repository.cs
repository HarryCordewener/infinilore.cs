// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Data.Models.Base;

namespace InfiniLore.Server.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class Repository<T>(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) where T : UserContent<T> {
    #region Queryables
    internal async Task<DbSet<T>> GetDbSetAsync() {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync();
        return dbContext.Set<T>();
    }
    #endregion
}
