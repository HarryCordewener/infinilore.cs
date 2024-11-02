// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Account;
using System.Security.Cryptography;
using System.Text;

namespace InfiniLore.Server.Data.Repositories.Query.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IJwtRefreshTokenQueries>(LifeTime.Scoped)]
public class JwtRefreshTokenQueries(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork) : IJwtRefreshTokenQueries {

    public async ValueTask<QueryOutput<JwtRefreshTokenModel>> TryGetByIdAsync(Guid refreshtoken, CancellationToken ct = default) {
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
}
