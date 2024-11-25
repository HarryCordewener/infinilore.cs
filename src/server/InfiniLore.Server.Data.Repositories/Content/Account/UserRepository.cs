// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using FastEndpoints;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.Account;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace InfiniLore.Server.Data.Repositories.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IUserRepository>(LifeTime.Scoped)]
public class UserRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork, UserManager<InfiniLoreUser> userManager) : IUserRepository {
    public async ValueTask<QueryResult<InfiniLoreUser>> TryGetByClaimsPrincipalAsync(ClaimsPrincipal principal, CancellationToken ct = default) {
        if (await userManager.GetUserAsync(principal) is not { } user) return new None();
        return user;
    }
    
    public async ValueTask<QueryResult<InfiniLoreUser>> TryGetByIdAsync(UserIdUnion userId, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        string id = userId.AsUserId;

        InfiniLoreUser? result = await dbContext.Users
            // .Include(u => u.Roles)
            // .Include(u => u.Permissions)
            .FirstOrDefaultAsync(u => u.Id == id, ct);

        if (result is null) return new None();

        return result;
    }

    public async ValueTask<QueryResult<InfiniLoreUser>> TryGetByUserNameAsync(string userName, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);

        InfiniLoreUser? result = await dbContext.Users
            // .Include(u => u.Roles)
            // .Include(u => u.Permissions)
            .FirstOrDefaultAsync(u => u.Id == userName, ct);

        if (result is null) return new None();

        return result;
    }

    public async ValueTask<QueryResultMany<InfiniLoreUser>> TryGetByQueryAsync(Expression<Func<InfiniLoreUser, bool>> predicate, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);

        InfiniLoreUser[] result = await dbContext.Users
            // .Include(u => u.Roles)
            // .Include(u => u.Permissions)
            .Where(predicate)
            .ToArrayAsync(ct);

        if (result.Length == 0) return new None();

        return result;
    }
}
