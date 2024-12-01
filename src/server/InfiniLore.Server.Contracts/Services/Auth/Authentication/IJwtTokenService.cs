// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.Account;

namespace InfiniLore.Server.Contracts.Services.Auth.Authentication;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJwtTokenService {
    ValueTask<JwtResult> GenerateTokensAsync(InfiniLoreUser user, string[] roles, string[] permissions, int? expiresInDays, CancellationToken ct = default);
    ValueTask<JwtResult> RefreshTokensAsync(Guid refreshToken, CancellationToken ct = default);
    ValueTask<BoolOrFailure> RevokeTokensAsync(InfiniLoreUser user, Guid refreshToken, CancellationToken ct = default);
    ValueTask<BoolOrFailure> RevokeAllTokensFromUserAsync(InfiniLoreUser user, CancellationToken ct = default);
}
