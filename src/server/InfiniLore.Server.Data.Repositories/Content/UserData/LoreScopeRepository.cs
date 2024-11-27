// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.DependencyInjection;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Server.Data.Repositories.Content.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<ILoreScopeRepository>(ServiceLifetime.Scoped)]
public class LoreScopeRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : UserContentRepository<LoreScopeModel>(unitOfWork), ILoreScopeRepository {
    public async ValueTask<RepoResult<LoreScopeModel[]>> TryGetByUserIdAndLorescopeId(UserIdUnion userId, Guid? lorescopeId, CancellationToken ct = default) {
        DbSet<LoreScopeModel> dbSet = await GetDbSetAsync(ct);

        LoreScopeModel[] result = await dbSet
            .Include(model => model.Multiverses)// Include the multiverses so we can collect the Ids
            .Where(model => model.OwnerId == userId.ToGuid() && model.Id == lorescopeId)
            .ToArrayAsync(cancellationToken: ct);

        return result;
    }
}
