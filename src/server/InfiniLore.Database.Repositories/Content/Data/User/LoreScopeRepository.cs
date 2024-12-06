// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.DependencyInjection;
using AterraEngine.Unions;
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
[InjectableService<ILorescopeRepository>(ServiceLifetime.Scoped)]
public class LorescopeRepository(IDbUnitOfWork<MsSqlDbContext> unitOfWork) : UserContentRepository<LorescopeModel>(unitOfWork), ILorescopeRepository {
    public async ValueTask<RepoResult<LorescopeModel[]>> TryGetByUserIdAndLorescopeIdAsync(UserIdUnion userId, Guid lorescopeId, CancellationToken ct = default) {
        DbSet<LorescopeModel> dbSet = await GetDbSetAsync(ct);

        LorescopeModel[] result = await dbSet
            .Include(model => model.Multiverses)// Include the multiverses so we can collect the Ids
            .Where(model => model.OwnerId == userId.ToGuid() && model.Id == lorescopeId)
            .ToArrayAsync(cancellationToken: ct);

        return result;
    }
    
    public async ValueTask<RepoResult> IsValidNewNameAsync(UserIdUnion userId, string name, CancellationToken ct = default) {
        DbSet<LorescopeModel> dbSet = await GetDbSetAsync(ct);
        
        LorescopeModel? existing = await dbSet
            .FirstOrDefaultAsync(predicate: model => model.OwnerId == userId.ToGuid() && model.Name == name, ct);
        
        if (existing != null) return "A lore scope with that name already exists";
        return new Success();
    }
}
