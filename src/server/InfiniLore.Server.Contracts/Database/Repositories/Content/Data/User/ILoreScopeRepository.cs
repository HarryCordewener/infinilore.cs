// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.UserData;
using UserIdUnion=InfiniLore.Server.Contracts.Types.UserIdUnion;

namespace InfiniLore.Server.Contracts.Database.Repositories.Content.Data.User;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ILorescopeRepository : IUserContentRepository<LorescopeModel> {
    ValueTask<RepoResult> IsValidNewNameAsync(UserIdUnion userId, string name, CancellationToken ct = default);
}
