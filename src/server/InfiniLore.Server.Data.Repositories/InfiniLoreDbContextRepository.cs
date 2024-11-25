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
    public async ValueTask<InfiniLoreDbContext> GetDbContextAsync() => await unitOfWork.GetDbContextAsync();
    
    protected async ValueTask<DbSet<T>> GetDbSetAsync() {
        InfiniLoreDbContext dbContext = await GetDbContextAsync();
        return dbContext.Set<T>();
    }
    #endregion
}
