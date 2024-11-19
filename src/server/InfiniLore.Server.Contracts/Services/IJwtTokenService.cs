// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Account;

namespace InfiniLore.Server.Contracts.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJwtTokenService {
    Task<JwtResult> GenerateTokensAsync(InfiniLoreUser user, string[] roles, string[] permissions, int? expiresInDays, CancellationToken ct = default);
    Task<JwtResult> RefreshTokensAsync(Guid refreshToken, CancellationToken ct = default);
    Task<TrueFalseOrError> RevokeTokensAsync(InfiniLoreUser user, Guid refreshToken, CancellationToken ct = default);
    Task<TrueFalseOrError> RevokeAllTokensFromUserAsync(InfiniLoreUser user, CancellationToken ct = default);
}
