// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace InfiniLore.Server.Data.Repositories;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UserContentRepository<T>(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : BaseContentRepository<T>(unitOfWork), IUserContentRepository<T> where T : UserContent {
    public async virtual ValueTask<QueryResultMany<T>> TryGetByUserAsync(UserUnion userUnion, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();
        T[] result = await dbSet
            .Where(ls => ls.OwnerId == userUnion.AsUserId)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryResultMany<T>> TryGetByUserAsync(UserUnion userUnion, PaginationInfo pageInfo, CancellationToken ct = default) {
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

    public async virtual ValueTask<QueryResultMany<T>> TryGetByUserWithUserAccessAsync(UserUnion ownerUnion, UserUnion accessorUnion, AccessKind level, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();

        T[] result = await dbSet
            .Where(
                model => model.OwnerId == ownerUnion.AsUserId
                    && model.UserAccess.Any(access => access.User.Id == accessorUnion.AsUserId && access.AccessKind == level)
            )
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }

    public async virtual ValueTask<QueryResultMany<T>> TryGetByUserWithUserAccessAsync(UserUnion ownerUnion, UserUnion accessorUnion, AccessKind level, PaginationInfo pageInfo, CancellationToken ct) {
        DbSet<T> dbSet = await GetDbSetAsync();

        T[] result = await dbSet
            .Where(
                model => model.OwnerId == ownerUnion.AsUserId
                    && model.UserAccess.Any(access => access.User.Id == accessorUnion.AsUserId && access.AccessKind == level)
            )
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result.Length > 0
            ? result
            : new None();
    }
}
