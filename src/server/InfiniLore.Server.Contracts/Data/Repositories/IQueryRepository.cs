// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Base;
using System.Linq.Expressions;

namespace InfiniLore.Server.Contracts.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IQueryRepository<T> :
    IQueryHasTryGetByIdAsync<T>,
    IQueryHasTryGetByUserAsync<T>,
    IQueryHasTryGetAllAsync<T>,
    IQueryHasTryGetByCriteriaAsync<T>
    where T : UserContent<T>;

public interface IQueryHasTryGetByIdAsync<T> where T : BaseContent<T> {
    ValueTask<QueryOutput<T>> TryGetByIdAsync(Guid id, CancellationToken ct = default);
}

public interface IQueryHasTryGetByUserAsync<T> where T : UserContent<T> {
    ValueTask<QueryOutputMany<T>> TryGetByUserAsync(UserUnion userUnion, CancellationToken ct = default);
    ValueTask<QueryOutputMany<T>> TryGetByUserAsync(UserUnion userUnion, PaginationInfo pageInfo, CancellationToken ct = default);
    ValueTask<QueryOutputMany<T>> TryGetByUserWithUserAccessAsync(UserUnion ownerUnion, UserUnion accessorUnion, AccessLevel level, CancellationToken ct = default);
    ValueTask<QueryOutputMany<T>> TryGetByUserWithUserAccessAsync(UserUnion ownerUnion, UserUnion accessorUnion, AccessLevel level, PaginationInfo pageInfo, CancellationToken ct = default);
}

public interface IQueryHasTryGetAllAsync<T> where T : BaseContent<T> {
    ValueTask<QueryOutputMany<T>> TryGetAllAsync(CancellationToken ct = default);
    ValueTask<QueryOutputMany<T>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct = default);
}

public interface IQueryHasTryGetByCriteriaAsync<T> where T : BaseContent<T> {
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct = default);

    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct = default, Expression<Func<T, object>>? orderBy = null);
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct = default, Expression<Func<T, object>>? orderBy = null);
}
