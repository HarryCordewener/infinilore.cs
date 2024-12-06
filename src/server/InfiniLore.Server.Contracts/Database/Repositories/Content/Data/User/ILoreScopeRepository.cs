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
public interface ILorescopeRepository : IUserContentRepository<LorescopeModel> {
    ValueTask<RepoResult> IsValidNewNameAsync(UserIdUnion userId, string name, CancellationToken ct = default);
}
