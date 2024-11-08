// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Account;
using System.Linq.Expressions;

namespace InfiniLore.Server.Contracts.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IUserRepository {
    ValueTask<QueryOutput<InfiniLoreUser>> TryGetByIdAsync(UserIdUnion id, CancellationToken ct = default);
    ValueTask<QueryOutput<InfiniLoreUser>> TryGetByUserNameAsync(string userName, CancellationToken ct = default);
    ValueTask<QueryOutputMany<InfiniLoreUser>> TryGetByQueryAsync(Expression<Func<InfiniLoreUser, bool>> predicate, CancellationToken ct = default);
}
