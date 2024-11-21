// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.Account;
using System.Linq.Expressions;

namespace InfiniLore.Server.Contracts.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserRepository {
    ValueTask<QueryResult<InfiniLoreUser>> TryGetByIdAsync(UserIdUnion id, CancellationToken ct = default);
    ValueTask<QueryResult<InfiniLoreUser>> TryGetByUserNameAsync(string userName, CancellationToken ct = default);
    ValueTask<QueryResultMany<InfiniLoreUser>> TryGetByQueryAsync(Expression<Func<InfiniLoreUser, bool>> predicate, CancellationToken ct = default);
}
