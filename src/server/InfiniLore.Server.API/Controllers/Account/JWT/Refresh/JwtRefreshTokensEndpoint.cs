// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Services;
using InfiniLore.Server.Contracts.Types;
using InfiniLore.Server.Contracts.Types.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Account.JWT.Refresh;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class JwtRefreshTokensEndpoint(IJwtTokenService jwtTokenService, ILogger logger)
    : Endpoint<
        JwtRefreshTokensRequest,
        Results<
            BadRequest<ProblemDetails>,
            Ok<JwtResponse>
        >
    > {
    public override void Configure() {
        Post("/account/jwt/token/refresh");
        AllowAnonymous();
    }
    public async override Task<Results<BadRequest<ProblemDetails>, Ok<JwtResponse>>> ExecuteAsync(JwtRefreshTokensRequest req, CancellationToken ct) {
        logger.Information("Generating tokens for refreshToken {@Token}", req.RefreshToken);
        
        JwtResult jwtResult = await jwtTokenService.RefreshTokensAsync(req.RefreshToken, ct);
        switch (jwtResult.Value) {
            case JwtTokenData data: {
                logger.Information("Tokens generated successfully for refreshToken {@Token}", req.RefreshToken);
                return TypedResults.Ok(new JwtResponse(
                    data.AccessToken, 
                    data.AccessTokenExpiryUtc, 
                    data.RefreshToken,
                    data.RefreshTokenExpiryUtc
                ));
            }
                
            default: {
                logger.Warning("Unable to generate tokens for refreshToken {@Token}. Result: {@JwtResult}", req.RefreshToken, jwtResult.ErrorString);
                return TypedResults.BadRequest(new ProblemDetails { Detail = "Unable to generate tokens." });   
            }
        }

    }
}
