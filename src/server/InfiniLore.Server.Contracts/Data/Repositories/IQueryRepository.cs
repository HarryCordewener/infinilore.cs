// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Data.Models.Base;
using InfiniLore.Server.Contracts.Types.Results;
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

    where T : UserContent<T>
    ;

public interface IQueryHasTryGetByIdAsync<T> where T : BaseContent<T>  {
    ValueTask<QueryOutput<T>> TryGetByIdAsync(Guid id, CancellationToken ct);
}

public interface IQueryHasTryGetByUserAsync<T> where T : UserContent<T> {
    ValueTask<QueryOutputMany<T>> TryGetByUserAsync(InfiniLoreUser user, CancellationToken ct);
    ValueTask<QueryOutputMany<T>> TryGetByUserAsync(Guid userId, CancellationToken ct);
    ValueTask<QueryOutputMany<T>> TryGetByUserAsync(string userId, CancellationToken ct);
}

public interface IQueryHasTryGetAllAsync<T> where T : BaseContent<T> {
    ValueTask<QueryOutputMany<T>> TryGetAllAsync(CancellationToken ct);
    ValueTask<QueryOutputMany<T>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct);
}

public interface IQueryHasTryGetByCriteriaAsync<T> where T : BaseContent<T> {
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct); 
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct); 
    
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null); 
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null); 
}