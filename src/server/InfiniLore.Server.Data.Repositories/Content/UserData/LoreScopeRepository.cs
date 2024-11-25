// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using FastEndpoints;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace InfiniLore.Server.Data.Repositories.Content.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<ILoreScopeRepository>(LifeTime.Scoped)]
public class LoreScopeRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : UserContentRepository<LoreScopeModel>(unitOfWork), ILoreScopeRepository {
    public async ValueTask<QueryResultMany<LoreScopeModel>> TryGetByUserIdAndLorescopeId(UserIdUnion userId, Guid? lorescopeId, CancellationToken ct = default) {
        DbSet<LoreScopeModel> dbSet = await GetDbSetAsync();
        
        LoreScopeModel[] result = await dbSet
            .Include(model => model.Multiverses) // Include the multiverses so we can collect the Ids
            .Where(model => model.OwnerId == userId.AsUserId &&  model.Id == lorescopeId)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }
}
