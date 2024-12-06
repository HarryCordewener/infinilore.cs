// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;

namespace InfiniLore.Server.Contracts.Database.Repositories.Content.Data.User;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ILoreScopeRepository : IUserContentRepository<LoreScopeModel> {
    ValueTask<RepoResult<LoreScopeModel[]>> TryGetByUserIdAndLorescopeIdAsync(UserIdUnion userId, Guid lorescopeId, CancellationToken ct = default);
    ValueTask<RepoResult> CanUseAsNewLorescopeNameAsync(UserIdUnion userId, string name, CancellationToken ct = default);
}
