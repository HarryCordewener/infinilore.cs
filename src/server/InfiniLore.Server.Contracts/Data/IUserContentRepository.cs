// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Base;

namespace InfiniLore.Server.Contracts.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserContentRepository<T> :
    IBaseContentRepository<T>,
    IQueryHasTryGetByUserAsync<T>
    where T : UserContent;

#region Commands
#endregion
#region Queries
#region Default
public interface IQueryHasTryGetByUserAsync<T> where T : UserContent {
    ValueTask<QueryResultMany<T>> TryGetByUserAsync(UserUnion userUnion, CancellationToken ct = default);
    ValueTask<QueryResultMany<T>> TryGetByUserAsync(UserUnion userUnion, PaginationInfo pageInfo, CancellationToken ct = default);
    ValueTask<QueryResultMany<T>> TryGetByUserWithUserAccessAsync(UserUnion ownerUnion, UserUnion accessorUnion, AccessLevel level, CancellationToken ct = default);
    ValueTask<QueryResultMany<T>> TryGetByUserWithUserAccessAsync(UserUnion ownerUnion, UserUnion accessorUnion, AccessLevel level, PaginationInfo pageInfo, CancellationToken ct = default);
}
#endregion
#endregion
