// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
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
    public async virtual ValueTask<RepoResult<T[]>> TryGetByUserAsync(UserIdUnion userUnion, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync(ct);
        T[] result = await dbSet
            .Where(ls => ls.OwnerId == userUnion.ToGuid())
            .ToArrayAsync(cancellationToken: ct);

        return result;
    }

    public async virtual ValueTask<RepoResult<T[]>> TryGetByUserAsync(UserIdUnion userUnion, PaginationInfo pageInfo, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync(ct);
        T[] result = await dbSet
            .Where(ls => ls.OwnerId == userUnion.ToGuid())
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result;
    }

    public async virtual ValueTask<RepoResult<T[]>> TryGetByUserWithUserAccessAsync(UserIdUnion ownerUnion, UserIdUnion accessorUnion, AccessKind level, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync(ct);

        T[] result = await dbSet
            .Where(
                model => model.OwnerId == ownerUnion.ToGuid()
                    && model.UserAccess.Any(access => access.UserId == accessorUnion.ToGuid() && access.AccessKind == level)
            )
            .ToArrayAsync(cancellationToken: ct);

        return result;
    }

    public async virtual ValueTask<RepoResult<T[]>> TryGetByUserWithUserAccessAsync(UserIdUnion ownerUnion, UserIdUnion accessorUnion, AccessKind level, PaginationInfo pageInfo, CancellationToken ct = default) {
        DbSet<T> dbSet = await GetDbSetAsync(ct);

        T[] result = await dbSet
            .Where(
                model => model.OwnerId == ownerUnion.ToGuid()
                    && model.UserAccess.Any(access => access.UserId == accessorUnion.ToGuid() && access.AccessKind == level)
            )
            .Skip(pageInfo.SkipAmount)
            .Take(pageInfo.PageSize)
            .ToArrayAsync(cancellationToken: ct);

        return result;
    }
}
