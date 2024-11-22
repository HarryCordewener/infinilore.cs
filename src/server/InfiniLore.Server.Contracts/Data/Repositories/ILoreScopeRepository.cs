// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;

namespace InfiniLore.Server.Contracts.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ILoreScopeRepository : IUserContentRepository<LoreScopeModel> {
    ValueTask<QueryResultMany<LoreScopeModel>> TryGetByUserIdAndLorescopeId(UserIdUnion userId, Guid? lorescopeId, CancellationToken ct = default);
}
