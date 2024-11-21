// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using AterraEngine.Unions;
using FastEndpoints;
using FastEndpoints.Security;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Services;
using InfiniLore.Server.Contracts.Types;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.Account;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InfiniLore.Server.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IJwtTokenService>(LifeTime.Scoped)]
public class JwtTokenService(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork, IConfiguration configuration, IJwtRefreshTokenRepository repository, ILogger logger, UserManager<InfiniLoreUser> userManager) : IJwtTokenService {

    public async Task<JwtResult> GenerateTokensAsync(InfiniLoreUser user, string[] roles, string[] permissions, int? expiresInDays, CancellationToken ct = default) {
        try {
            string? key = configuration["Jwt:Key"];
            if (key == null) {
                logger.Error("Jwt:Key not found in configuration");
                return "Jwt:Key not found in configuration";
            }

            // Check if all provided roles exist in the user's roles
            IList<string> userRoles = await userManager.GetRolesAsync(user);
            if (!roles.All(role => userRoles.Contains(role))) {
                logger.Warning("User {UserId} does not have all specified roles", user.Id);
                return "User does not have the required roles";
            }

            DateTime accessTokenExpiryUtc = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:AccessExpiresInMinutes"]!));
            DateTime refreshTokenExpiryUtc = DateTime.UtcNow.AddDays(expiresInDays ?? int.Parse(configuration["Jwt:RefreshExpiresInDays"]!));

            string accessToken = GenerateAccessToken(user, roles, permissions, accessTokenExpiryUtc);
            Union<Guid, False> resultGenerate = await GenerateRefreshTokenAsync(user, roles, permissions, refreshTokenExpiryUtc, ct);
            if (resultGenerate.IsT1) return "Refresh token could not be generated";

            return new JwtTokenData(
                accessToken,
                accessTokenExpiryUtc,
                resultGenerate.AsT0,
                refreshTokenExpiryUtc);

        }
        catch (Exception ex) {
            logger.Error(ex, "Error generating tokens");
            return "Unexpected error generating tokens";
        }
    }

    public async Task<JwtResult> RefreshTokensAsync(Guid refreshToken, CancellationToken ct = default) {
        QueryResult<JwtRefreshTokenModel> getResult = await repository.TryGetByIdAsync(refreshToken, ct);
        switch (getResult.Value) {
            case None: return "Refresh token not found";
            case Error<string>: return getResult.ErrorString;
        }

        JwtRefreshTokenModel oldToken = getResult.AsSuccess.Value;
        if (oldToken.ExpiresAt < DateTime.UtcNow) return "Refresh token has expired";

        await repository.TryPermanentDeleteAsync(oldToken, ct);

        return await GenerateTokensAsync(
            oldToken.Owner,
            oldToken.Roles,
            oldToken.Permissions,
            oldToken.ExpiresInDays ?? int.Parse(configuration["Jwt:RefreshExpiresInDays"]!),
            ct
        );
    }

    public async Task<TrueFalseOrError> RevokeTokensAsync(InfiniLoreUser user, Guid refreshToken, CancellationToken ct = default) {
        QueryResult<JwtRefreshTokenModel> getResult = await repository.TryGetByIdAsync(refreshToken, ct);
        switch (getResult.Value) {
            case None: return "Refresh token not found";
            case Error<string>: return getResult.ErrorString;
        }

        JwtRefreshTokenModel oldToken = getResult.AsSuccess.Value;
        if (oldToken.Owner.Id != user.Id) return "Refresh token does not belong to user";

        CommandOutput deleteResult = await repository.TryPermanentDeleteAsync(oldToken, ct);
        if (!deleteResult.IsSuccess) return deleteResult.ErrorString;

        return true;
    }

    public async Task<TrueFalseOrError> RevokeAllTokensFromUserAsync(InfiniLoreUser user, CancellationToken ct = default) {
        CommandOutput deleteResult = await repository.TryPermanentDeleteAllForUserAsync(user, ct);
        if (!deleteResult.IsSuccess) return deleteResult.ErrorString;

        return true;

    }
    private static string HashToken(Guid token) {
        byte[] tokenBytes = Encoding.UTF8.GetBytes(token.ToString());
        byte[] hashBytes = SHA256.HashData(tokenBytes);
        return Convert.ToBase64String(hashBytes);
    }

    private string GenerateAccessToken(InfiniLoreUser user, string[] roles, string[] permissions, DateTime expiresAt) {
        string jwtToken = JwtBearer.CreateToken(
            o => {
                o.SigningKey = configuration["Jwt:Key"]!;
                o.ExpireAt = expiresAt;
                o.Audience = configuration["JWT:Audience"];
                o.Issuer = configuration["JWT:Issuer"];

                o.User.Roles.Add(roles);
                o.User.Permissions.Add(permissions);
                o.User[ClaimTypes.NameIdentifier] = user.Id;
            });

        return jwtToken;
    }

    private async Task<Union<Guid, False>> GenerateRefreshTokenAsync(InfiniLoreUser user, string[] roles, string[] permissions, DateTime expiresAt, CancellationToken ct = default) {
        var token = Guid.NewGuid();
        var refreshToken = new JwtRefreshTokenModel {
            Owner = user,
            ExpiresAt = expiresAt,
            TokenHash = HashToken(token),
            Roles = roles,
            Permissions = permissions
        };

        CommandOutput resultAddition = await repository.TryAddAsync(refreshToken, ct);
        switch (resultAddition.Value) {
            case Success:
                try {
                    logger.Information("Attempting to commit transaction for user {UserId}", user.Id);
                    await unitOfWork.CommitAsync(ct);
                    logger.Information("Transaction committed successfully for user {UserId}", user.Id);
                    return token;
                }
                catch (Exception ex) {
                    logger.Error(ex, "Error during commit for user {UserId}", user.Id);
                    return new False();
                }

            default:
                logger.Error("Error adding refresh token for user {UserId}", user.Id);
                return new False();
        }
    }
}
