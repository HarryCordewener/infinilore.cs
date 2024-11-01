// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Data.Models.Base;
using InfiniLoreLib.Results;
using System.Linq.Expressions;

namespace InfiniLore.Server.Contracts.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IQueryRepository<T> where T : UserContent<T> {
    ValueTask<QueryOutput<T>> TryGetByIdAsync(Guid id, CancellationToken ct);
    
    ValueTask<QueryOutputMany<T>> TryGetByUserAsync(InfiniLoreUser user, CancellationToken ct);
    ValueTask<QueryOutputMany<T>> TryGetByUserAsync(Guid userId, CancellationToken ct);
    ValueTask<QueryOutputMany<T>> TryGetByUserAsync(string userId, CancellationToken ct);
    
    ValueTask<QueryOutputMany<T>> TryGetAllAsync(CancellationToken ct);
    ValueTask<QueryOutputMany<T>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct);
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct); 
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct); 
    
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null); 
    ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null); 
}