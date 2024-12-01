// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.DependencyInjection;
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Database.MsSqlServer;
using InfiniLore.Server.Contracts.Database;
using InfiniLore.Server.Contracts.Database.Repositories.Content.Data.User;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.Database.Repositories.Content.Data.User;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<ILoreScopeRepository>(ServiceLifetime.Scoped)]
public class LoreScopeRepository(IDbUnitOfWork<MsSqlDbContext> unitOfWork) : UserContentRepository<LoreScopeModel>(unitOfWork), ILoreScopeRepository {
    public async ValueTask<RepoResult<LoreScopeModel[]>> TryGetByUserIdAndLorescopeId(UserIdUnion userId, Guid lorescopeId, CancellationToken ct = default) {
        DbSet<LoreScopeModel> dbSet = await GetDbSetAsync(ct);

        LoreScopeModel[] result = await dbSet
            .Include(model => model.Multiverses)// Include the multiverses so we can collect the Ids
            .Where(model => model.OwnerId == userId.ToGuid() && model.Id == lorescopeId)
            .ToArrayAsync(cancellationToken: ct);

        return result;
    }
}
