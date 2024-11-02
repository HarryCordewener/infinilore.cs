// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using CodeOfChaos.Extensions;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Base;
using OneOf.Types;
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
    public async virtual ValueTask<QueryOutput<T>> TryGetByIdAsync(Guid id, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T? result = await dbSet
            .FirstOrDefaultAsync(predicate: ls => ls.Id == id, ct);

        if (result is null) return new None();

        return new Success<T>(result);
    }

    public async virtual ValueTask<QueryOutputMany<T>> TryGetByUserAsync(UserUnion userUnion, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(ls => ls.OwnerId == userUnion.AsUserId)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async ValueTask<QueryOutputMany<T>> TryGetByUserAsync(UserUnion userUnion, PaginationInfo pageInfo, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(ls => ls.OwnerId == userUnion.AsUserId)
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async ValueTask<QueryOutputMany<T>> TryGetByUserWithUserAccessAsync(UserUnion ownerUnion, UserUnion accessorUnion, AccessLevel level, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();

        T[] result = await dbSet
            .Where(
                model => model.OwnerId == ownerUnion.AsUserId
                    && model.UserAccess.Any(access => access.User.Id == accessorUnion.AsUserId && access.AccessLevel == level)
            )
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async ValueTask<QueryOutputMany<T>> TryGetByUserWithUserAccessAsync(UserUnion ownerUnion, UserUnion accessorUnion, AccessLevel level, PaginationInfo pageInfo, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();

        T[] result = await dbSet
            .Where(
                model => model.OwnerId == ownerUnion.AsUserId
                    && model.UserAccess.Any(access => access.User.Id == accessorUnion.AsUserId && access.AccessLevel == level)
            )
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryOutputMany<T>> TryGetAllAsync(CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryOutputMany<T>> TryGetAllASync(PaginationInfo pageInfo, CancellationToken ct) {
        if (pageInfo.IsNotValid(out Error<string> pageInfoError)) return pageInfoError;

        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null) {
        if (pageInfo.IsNotValid(out Error<string> pageInfoError)) return pageInfoError;

        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ConditionalOrderByNotNull(orderBy)// Checks for not null and then orders by it, else skips the order by
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryOutputMany<T>> TryGetByCriteriaAsync(Expression<Func<T, int, bool>> predicate, PaginationInfo pageInfo, CancellationToken ct, Expression<Func<T, object>>? orderBy = null) {
        if (pageInfo.IsNotValid(out Error<string> pageInfoError)) return pageInfoError;

        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(predicate)
            .ConditionalOrderByNotNull(orderBy)// Checks for not null and then orders by it, else skips the order by
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
        #endregion
    }
}
