// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Data;
using InfiniLore.Server.Data.Models.UserData;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InfiniLore.Server.API.Controllers.LoreScopes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class SeedLoreScopes(ILoreScopesCommands loreScopesCommands, ILogger logger, IDbUnitOfWork<InfiniLoreDbContext> dbUnitOfWork ) : EndpointWithoutRequest {
    public override void Configure() {
        Get("/lore-scopes/seed");
        Roles("Admin");
    }

    public async override Task HandleAsync(CancellationToken ct) {
        logger.Information("User is authenticated: {IsAuthenticated}", User.Identity is { IsAuthenticated: true });
        logger.Information("User roles: {Roles}", string.Join(", ", User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)));

        InfiniLoreDbContext db = await dbUnitOfWork.GetDbContextAsync(ct);
        if (await db.Users.FirstOrDefaultAsync(predicate: u => u.UserName == "testuser", ct) is not {} user) return;

        await loreScopesCommands.TryAddRangeAsync([
            new LoreScopeModel { Owner = user, Name = "A" },
            new LoreScopeModel { Owner = user, Name = "B" },
            new LoreScopeModel { Owner = user, Name = "C" },
            new LoreScopeModel { Owner = user, Name = "D" },
            new LoreScopeModel { Owner = user, Name = "E" },
        ], ct);

        await dbUnitOfWork.CommitAsync(ct);
    }
}
