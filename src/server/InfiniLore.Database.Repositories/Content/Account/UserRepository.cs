// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.DependencyInjection;
using InfiniLore.Database.Models.Content.Account;
using InfiniLore.Database.MsSqlServer;
using InfiniLore.Server.Contracts.Database;
using InfiniLore.Server.Contracts.Database.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using UserIdUnion=InfiniLore.Server.Contracts.Types.UserIdUnion;

namespace InfiniLore.Database.Repositories.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<IUserRepository>(ServiceLifetime.Scoped)]
public class UserRepository(
    IDbUnitOfWork<MsSqlDbContext> unitOfWork,
    UserManager<InfiniLoreUser> userManager
) : IUserRepository {
    /// <inheritdoc />
    public async ValueTask<RepoResult<InfiniLoreUser>> TryGetByClaimsPrincipalAsync(ClaimsPrincipal principal, CancellationToken ct = default) {
        if (await userManager.GetUserAsync(principal) is not {} user) return "User not found.";

        return user;
    }

    /// <inheritdoc />
    public async ValueTask<RepoResult<InfiniLoreUser>> TryGetByIdAsync(UserIdUnion userId, CancellationToken ct = default) {
        MsSqlDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        var id = userId.ToGuid();

        InfiniLoreUser? result = await dbContext.Users
            .FirstOrDefaultAsync(predicate: u => u.Id == id, ct);

        if (result is null) return "User not found.";

        return result;
    }
}
