// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using FastEndpoints;
using InfiniLore.Server.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace InfiniLore.Server.Services;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[RegisterService<IJwtParsingService>(LifeTime.Scoped)]
public class JwtParsingService(IHttpContextAccessor contextAccessor, ILogger logger) : IJwtParsingService {

    private readonly JwtSecurityTokenHandler _handler = new();

    // ReSharper disable once RedundantDefaultMemberInitializer
    private bool? _gotValidToken = null;
    private JwtSecurityToken? _jwt;
    public JwtSecurityToken? Jwt {
        get {
            switch (_gotValidToken) {
                case true: return _jwt;
                case false: return null;

                default: {
                    _gotValidToken = TryParseJwt(out JwtSecurityToken? jwt);
                    return _jwt = jwt;
                }
            }
        }
        private set => _jwt = value;
    }

    public bool TryParseJwt([NotNullWhen(true)] out JwtSecurityToken? jwt) {
        jwt = null;

        if (contextAccessor.HttpContext is not {} httpContext) return false;
        if (!httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authorizationHeader)) return false;

        string token = authorizationHeader
            .ToString()
            .Replace("Bearer ", string.Empty);

        if (!_handler.CanReadToken(token)) return false;

        return (Jwt = jwt = _handler.ReadJwtToken(token)) is not null;
    }

    public bool TryGetPermissions([NotNullWhen(true)] out string[]? permissions) => TryGetPayloadData("permissions", out permissions);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private bool TryGetPayloadData<T>(string key, [NotNullWhen(true)] out T? value) {
        value = default;

        if (Jwt is null) return false;
        if (!Jwt.Payload.TryGetValue(key, out object? objectValue)) return false;

        try {
            switch (objectValue) {
                case T objectString when typeof(T) == typeof(string): {
                    value = objectString;
                    return true;
                }

                case JsonElement permissionsJson: {
                    value = JsonSerializer.Deserialize<T>(permissionsJson.GetRawText());
                    return value is not null;
                }

                default:
                    return false;
            }
        }
        catch (Exception ex) {
            logger.Error(ex, "Error parsing for {Key} at {Value}", key, objectValue);
            return false;
        }
    }
    public bool TryGetRoles([NotNullWhen(true)] out string[]? permissions) => TryGetPayloadData("roles", out permissions);
}
