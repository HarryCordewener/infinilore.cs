// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using FastEndpoints;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace InfiniLore.Server.Data.Repositories;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IUserContentAccessRepository>(LifeTime.Scoped)]
public class UserContentAccessRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : IUserContentAccessRepository {
    public async ValueTask<bool> UserHasKindAsync(Guid contentId, UserIdUnion accessorId, AccessKind accessKind, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        UserContentAccessModel[] potentialAccesses = await dbContext.UserContentAccesses.Where(
            access => access.ContentId == contentId
                && access.UserId == accessorId.AsUserId
            ).ToArrayAsync(cancellationToken: ct);
        return potentialAccesses.Any(access => access.AccessKind.HasFlag(accessKind));
    }
}
