// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;

namespace InfiniLore.Server.Contracts.Services.Auth.Authentication;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IJwtParsingService {
    JwtSecurityToken? Jwt { get; }
    bool TryParseJwt([NotNullWhen(true)] out JwtSecurityToken? jwt);
    bool TryGetPermissions([NotNullWhen(true)] out string[]? roles);
    bool TryGetRoles([NotNullWhen(true)] out string[]? permissions);
}
