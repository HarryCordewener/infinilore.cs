// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Base;
using System.Linq.Expressions;

namespace InfiniLore.Server.Contracts.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IBaseContentRepository<T> :
    ICommandHasTryAddAsync<T>,
    ICommandHasTryUpdateAsync<T>,
    ICommandHasTryAddOrUpdateAsync<T>,
    ICommandHasTryDeleteAsync<T>,
    IQueryHasTryGetByIdAsync<T>,
    IQueryHasTryGetAllAsync<T>,
    IQueryHasTryGetByCriteriaAsync<T>
    where T : BaseContent<T>;

#region Commands
#region Default
public interface ICommandHasTryAddAsync<T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryAddAsync(T model, CancellationToken ct = default);
    ValueTask<CommandResult<T>> TryAddWithResultAsync(T model, CancellationToken ct = default);
    ValueTask<CommandOutput> TryAddRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}

public interface ICommandHasTryUpdateAsync<T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default);
    ValueTask<CommandResult<T>> TryUpdateWithResultAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default);
}

public interface ICommandHasTryAddOrUpdateAsync<T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryAddOrUpdateAsync(T model, Func<T, ValueTask<T>> update, CancellationToken ct = default);
    ValueTask<CommandOutput> TryAddOrUpdateRangeAsync(IEnumerable<T> models, Func<T, ValueTask<T>> update, CancellationToken ct = default);
}

public interface ICommandHasTryDeleteAsync<in T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryDeleteAsync(T model, CancellationToken ct = default);
    ValueTask<CommandOutput> TryDeleteRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}
#endregion
#region Special
public interface ICommandHasTryPermanentDeleteAsync<in T> where T : BaseContent<T> {
    ValueTask<CommandOutput> TryPermanentDeleteAsync(T model, CancellationToken ct = default);
    ValueTask<CommandOutput> TryPermanentDeleteRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}

public interface ICommandHasTryPermanentDeleteAllForUserAsync<in T> where T : BaseContent<T>, IHasOwner {
    ValueTask<CommandOutput> TryPermanentDeleteAllForUserAsync(UserUnion userUnion, CancellationToken ct = default);
}
#endregion
#endregion
#region Queries
#region Default
public interface IQueryHasTryGetByIdAsync<T> where T : BaseContent<T> {
    ValueTask<QueryResult<T>> TryGetByIdAsync(Guid id, CancellationToken ct = default);
}

public interface IQueryHasTryGetAllAsync<T> where T : BaseContent<T> {
    ValueTask<QueryResultMany<T>> TryGetAllAsync(CancellationToken ct = default);
    ValueTask<QueryResultMany<T>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct = default);
}

public interface IQueryHasTryGetByCriteriaAsync<T> where T : BaseContent<T> {
    ValueTask<QueryResultMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    ValueTask<QueryResultMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct = default);

    ValueTask<QueryResultMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct = default, Expression<Func<T, object>>? orderBy = null);
    ValueTask<QueryResultMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct = default, Expression<Func<T, object>>? orderBy = null);
}
#endregion
#endregion
