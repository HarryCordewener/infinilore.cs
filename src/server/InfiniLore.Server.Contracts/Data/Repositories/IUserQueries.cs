// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.Account;
using System.Linq.Expressions;
using System.Security.Claims;

namespace InfiniLore.Server.Contracts.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserRepository {
    ValueTask<RepoResult<InfiniLoreUser>> TryGetByClaimsPrincipalAsync(ClaimsPrincipal principal, CancellationToken ct = default);
    ValueTask<RepoResult<InfiniLoreUser>> TryGetByIdAsync(UserIdUnion userId, CancellationToken ct = default);
    ValueTask<RepoResult<InfiniLoreUser>> TryGetByUserNameAsync(string userName, CancellationToken ct = default);
    ValueTask<RepoResult<InfiniLoreUser[]>> TryGetByQueryAsync(Expression<Func<InfiniLoreUser, bool>> predicate, CancellationToken ct = default);
}
