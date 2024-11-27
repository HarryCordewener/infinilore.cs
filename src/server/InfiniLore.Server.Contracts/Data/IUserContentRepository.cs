// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models;

namespace InfiniLore.Server.Contracts.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserContentRepository<T> :
    IBaseContentRepository<T>,
    IQueryHasTryGetByUserAsync<T>
    where T : UserContent;

#region Queries
#region Default
public interface IQueryHasTryGetByUserAsync<T> where T : UserContent {
    ValueTask<QueryResultMany<T>> TryGetByUserAsync(UserIdUnion userUnion, CancellationToken ct = default);
    ValueTask<QueryResultMany<T>> TryGetByUserAsync(UserIdUnion userUnion, PaginationInfo pageInfo, CancellationToken ct = default);
    ValueTask<QueryResultMany<T>> TryGetByUserWithUserAccessAsync(UserIdUnion ownerUnion, UserIdUnion accessorUnion, AccessKind level, CancellationToken ct = default);
    ValueTask<QueryResultMany<T>> TryGetByUserWithUserAccessAsync(UserIdUnion ownerUnion, UserIdUnion accessorUnion, AccessKind level, PaginationInfo pageInfo, CancellationToken ct = default);
}
#endregion
#endregion
