// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Base;
using OneOf.Types;

namespace InfiniLore.Server.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UserContentRepository<T>(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : BaseContentRepository<T>(unitOfWork), IUserContentRepository<T> where T : UserContent<T> {
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
}
