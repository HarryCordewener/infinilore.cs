// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models;
using System.Linq.Expressions;

namespace InfiniLore.Server.Contracts.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IBaseContentRepository<T> :
    IHasTryAddAsync<T>,
    IHasTryUpdateAsync<T>,
    IHasTryAddOrUpdateAsync<T>,
    IHasTryDeleteAsync<T>,
    IHasTryRemoveAsync<T>,
    IHasTryGetByIdAsync<T>,
    IHasTryGetAllAsync<T>,
    IHasTryGetByCriteriaAsync<T>
    where T : BaseContent;

#region Commands
#region Default
public interface IHasTryAddAsync<T> where T : BaseContent {
    ValueTask<RepoResult> TryAddAsync(T model, CancellationToken ct = default);
    ValueTask<RepoResult<T>> TryAddWithResultAsync(T model, CancellationToken ct = default);
    ValueTask<RepoResult> TryAddRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}

public interface IHasTryUpdateAsync<T> where T : BaseContent {
    ValueTask<RepoResult> TryUpdateAsync(Guid id, Func<T, ValueTask<T>> update, CancellationToken ct = default);
    ValueTask<RepoResult<T>> TryUpdateWithResultAsync(Guid id, Func<T, ValueTask<T>> update, CancellationToken ct = default);
    ValueTask<RepoResult> TryUpdateAsync(T model, CancellationToken ct = default);
    ValueTask<RepoResult<T>> TryUpdateWithResultAsync(T model, CancellationToken ct = default);
    ValueTask<RepoResult> TryUpdateAsync(IEnumerable<T> models, CancellationToken ct = default);
}

public interface IHasTryAddOrUpdateAsync<T> where T : BaseContent {
    ValueTask<RepoResult> TryAddOrUpdateAsync(Guid id, Func<T, ValueTask<T>> update, CancellationToken ct = default);
    ValueTask<RepoResult> TryAddOrUpdateAsync(T model, CancellationToken ct = default);
    ValueTask<RepoResult> TryAddOrUpdateRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}

public interface IHasTryDeleteAsync<in T> where T : BaseContent {
    ValueTask<RepoResult> TryDeleteAsync(T model, CancellationToken ct = default);
    ValueTask<RepoResult> TryDeleteRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}

public interface IHasTryRemoveAsync<in T> where T : BaseContent {
    ValueTask<RepoResult> TryPermanentRemoveAsync(T model, CancellationToken ct = default);
    ValueTask<RepoResult> TryPermanentRemoveRangeAsync(IEnumerable<T> models, CancellationToken ct = default);
}
#endregion
#region Special
public interface IHasTryPermanentRemoveAllForUserAsync {
    ValueTask<RepoResult> TryPermanentRemoveAllForUserAsync(UserIdUnion userUnion, CancellationToken ct = default);
}
#endregion
#endregion
#region Queries
#region Default
public interface IHasTryGetByIdAsync<T> where T : BaseContent {
    ValueTask<RepoResult<T>> TryGetByIdAsync(Guid id, CancellationToken ct = default);
}

public interface IHasTryGetAllAsync<T> where T : BaseContent {
    ValueTask<RepoResult<T[]>> TryGetAllAsync(CancellationToken ct = default);
    ValueTask<RepoResult<T[]>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct = default);
}

public interface IHasTryGetByCriteriaAsync<T> where T : BaseContent {
    ValueTask<RepoResult<T[]>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    ValueTask<RepoResult<T[]>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct = default);

    ValueTask<RepoResult<T[]>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, PaginationInfo pageInfo, CancellationToken ct = default);
    ValueTask<RepoResult<T[]>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, Expression<Func<T, object>> orderBy, PaginationInfo pageInfo, CancellationToken ct = default);
}
#endregion
#endregion
