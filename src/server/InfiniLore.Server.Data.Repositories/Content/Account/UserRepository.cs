// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.DependencyInjection;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.Account;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System.Security.Claims;

namespace InfiniLore.Server.Data.Repositories.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<IUserRepository>(ServiceLifetime.Scoped)]
public class UserRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork, UserManager<InfiniLoreUser> userManager) : IUserRepository {
    public async ValueTask<RepoResult<InfiniLoreUser>> TryGetByClaimsPrincipalAsync(ClaimsPrincipal principal, CancellationToken ct = default) {
        if (await userManager.GetUserAsync(principal) is not {} user) return "User not found.";

        return user;
    }

    public async ValueTask<RepoResult<InfiniLoreUser>> TryGetByIdAsync(UserIdUnion userId, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        var id = userId.ToGuid();

        InfiniLoreUser? result = await dbContext.Users
            .FirstOrDefaultAsync(predicate: u => u.Id == id, ct);

        if (result is null) return "User not found.";

        return result;
    }

    public async ValueTask<RepoResult<InfiniLoreUser>> TryGetByUserNameAsync(string userName, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);

        InfiniLoreUser? result = await dbContext.Users
            .FirstOrDefaultAsync(predicate: u => u.NormalizedUserName != null
                && u.NormalizedUserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase
                ), ct);

        if (result is null) return "User not found.";

        return result;
    }

    public async ValueTask<RepoResult<InfiniLoreUser[]>> TryGetByQueryAsync(Expression<Func<InfiniLoreUser, bool>> predicate, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);

        InfiniLoreUser[] result = await dbContext.Users
            .Where(predicate)
            .ToArrayAsync(ct);

        if (result.Length == 0) return "User not found.";

        return result;
    }
}
