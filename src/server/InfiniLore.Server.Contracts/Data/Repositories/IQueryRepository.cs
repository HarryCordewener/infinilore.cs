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
    Task<Result<T>> TryGetByIdAsync(Guid id, CancellationToken ct);
    
    Task<Result<T[]>> TryGetByUserAsync(InfiniLoreUser user, CancellationToken ct);
    Task<Result<T[]>> TryGetByUserAsync(string userId, CancellationToken ct);
    
    Task<Result<T[]>> TryGetAllAsync(CancellationToken ct);
    Task<Result<T[]>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct);
    Task<Result<T[]>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct); 
    Task<Result<T[]>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct); 
    
    Task<Result<T[]>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null); 
    Task<Result<T[]>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null); 
}