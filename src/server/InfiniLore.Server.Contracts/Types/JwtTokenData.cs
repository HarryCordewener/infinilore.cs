// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Server.Contracts.Types;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record struct JwtTokenData(
    string AccessToken,
    DateTime AccessTokenExpiryUtc,
    Guid RefreshToken,
    DateTime RefreshTokenExpiryUtc
);
