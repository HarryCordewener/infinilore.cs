// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Data.Models.Account;
using InfiniLore.Server.Data.Models.Base;
using InfiniLoreLib.Results;
using System.Linq.Expressions;

namespace InfiniLore.Server.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class QueryRepository<T>(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : Repository<T>(unitOfWork), IQueryRepository<T> where T : UserContent<T> {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Results
    public async virtual Task<Result<T>> TryGetByIdAsync(Guid id, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? result = await dbSet
            .FirstOrDefaultAsync(ls => ls.Id == id, cancellationToken: ct);
        
        return result is not null
            ? Result<T>.Success(result)
            : Result<T>.Failure($"{nameof(T)} not found by criteria: {nameof(id)} == {id}");
    }
    
    public async virtual Task<Result<T[]>> TryGetByUserAsync(InfiniLoreUser user, CancellationToken ct) => 
        await TryGetByUserAsync(user.Id, ct);
    
    public async virtual Task<Result<T[]>> TryGetByUserAsync(string userId, CancellationToken ct) {DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(ls => ls.OwnerId == userId)
            .ToArrayAsync(cancellationToken: ct);
        
        return result.Length > 0
            ? Result<T[]>.Success(result)
            : Result<T[]>.Failure($"{nameof(T)} no items were found");
    }

    public async virtual Task<Result<T[]>> TryGetAllAsync(CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .ToArrayAsync(cancellationToken: ct);
        
        return result.Length > 0
            ? Result<T[]>.Success(result)
            : Result<T[]>.Failure($"{nameof(T)} no items were found");
    }
    
    public async virtual Task<Result<T[]>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(ct);
        
        return result.Length > 0
            ? Result<T[]>.Success(result)
            : Result<T[]>.Failure($"{nameof(T)} no items were found");
    }

    public async virtual Task<Result<T[]>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ToArrayAsync(cancellationToken: ct);
        
        return result.Length > 0
            ? Result<T[]>.Success(result)
            : Result<T[]>.Failure($"{nameof(T)} not found by criteria: {predicate}");
    }
    
    public async virtual Task<Result<T[]>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ToArrayAsync(cancellationToken: ct);
        
        return result.Length > 0
            ? Result<T[]>.Success(result)
            : Result<T[]>.Failure($"{nameof(T)} not found by criteria: {predicate}");
    }
    
    public async virtual Task<Result<T[]>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null) {
        if (pageInfo.IsNotValid()) {
            return Result<T[]>.Failure($"{nameof(pageInfo)} is not valid");
        }
        
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ConditionalOrderByNotNull(orderBy) // Checks for not null and then orders by it, else skips the order by
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);
        
        return result.Length > 0
            ? Result<T[]>.Success(result)
            : Result<T[]>.Failure($"{nameof(T)} not found by criteria: {predicate}");
    }
    
    public async virtual Task<Result<T[]>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null) {
        if (pageInfo.IsNotValid()) {
            return Result<T[]>.Failure($"{nameof(pageInfo)} is not valid");
        }
        
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ConditionalOrderByNotNull(orderBy) // Checks for not null and then orders by it, else skips the order by
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);
        
        return result.Length > 0
            ? Result<T[]>.Success(result)
            : Result<T[]>.Failure($"{nameof(T)} not found by criteria: {predicate}");
        #endregion
    }
    
    
}
