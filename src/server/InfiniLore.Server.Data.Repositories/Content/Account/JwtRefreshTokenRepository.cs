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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography;
using System.Text;

namespace InfiniLore.Server.Data.Repositories.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IJwtRefreshTokenRepository>(LifeTime.Scoped)]
public class JwtRefreshTokenRepository(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : IJwtRefreshTokenRepository {

    #region Commands
    public async ValueTask<CommandOutput> TryAddAsync(JwtRefreshTokenModel model, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        if (await dbContext.JwtRefreshTokens.AnyAsync(predicate: m => m.Id == model.Id, ct)) return "Model already exists";

        await dbContext.JwtRefreshTokens.AddAsync(model, ct);
        return new Success();
    }

    public async ValueTask<CommandResult<JwtRefreshTokenModel>> TryAddWithResultAsync(JwtRefreshTokenModel model, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        if (await dbContext.JwtRefreshTokens.AnyAsync(predicate: m => m.Id == model.Id, ct)) return "Model already exists";

        EntityEntry<JwtRefreshTokenModel> result = await dbContext.JwtRefreshTokens.AddAsync(model, ct);
        return result;
    }

    public async ValueTask<CommandOutput> TryAddRangeAsync(IEnumerable<JwtRefreshTokenModel> models, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        if (await dbContext.JwtRefreshTokens.AnyAsync(predicate: m => models.Any(m2 => m2.Id == m.Id), ct)) return "One or more Models already exist";

        await dbContext.JwtRefreshTokens.AddRangeAsync(models, ct);
        return new Success();
    }

    public async ValueTask<CommandOutput> TryPermanentDeleteAsync(JwtRefreshTokenModel model, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        JwtRefreshTokenModel? existing = await dbContext.JwtRefreshTokens.FindAsync([model.Id], ct);

        if (existing == null) return "Model does not exist";

        dbContext.JwtRefreshTokens.Remove(existing);
        return new Success();
    }

    public async ValueTask<CommandOutput> TryPermanentDeleteRangeAsync(IEnumerable<JwtRefreshTokenModel> models, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        int recordsAffected = await dbContext.JwtRefreshTokens.ExecuteDeleteAsync(cancellationToken: ct);

        if (recordsAffected <= 0) return "No models were deleted";

        return new Success();
    }

    public async ValueTask<CommandOutput> TryPermanentDeleteAllForUserAsync(UserUnion userUnion, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);

        int recordsAffected = await dbContext.JwtRefreshTokens
            .Where(m => m.OwnerId == userUnion.AsUserId)
            .ExecuteDeleteAsync(cancellationToken: ct);

        if (recordsAffected <= 0) return "No models were deleted";

        return new Success();
    }
    #endregion

    #region Queries
    public async ValueTask<QueryResult<JwtRefreshTokenModel>> TryGetByIdAsync(Guid refreshtoken, CancellationToken ct = default) {
        InfiniLoreDbContext dbContext = await unitOfWork.GetDbContextAsync(ct);
        string hashedToken = HashToken(refreshtoken);

        JwtRefreshTokenModel? tokenData = await dbContext.JwtRefreshTokens
            .Include(t => t.Owner)
            .FirstOrDefaultAsync(predicate: t => t.TokenHash == hashedToken, ct);

        if (tokenData == null) {
            return "Token not found.";
        }

        return tokenData;
    }
    private static string HashToken(Guid token) {
        byte[] tokenBytes = Encoding.UTF8.GetBytes(token.ToString());
        byte[] hashBytes = SHA256.HashData(tokenBytes);
        return Convert.ToBase64String(hashBytes);
    }
    #endregion
}
