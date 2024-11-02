// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

using InfiniLore.Server.Contracts.Services;
using InfiniLore.Server.Contracts.Types;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace InfiniLore.Server.API.Controllers.Account.JWT.Create;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class JwtCreateTokensEndpoint(IApiSignInService apiSignInService, IJwtTokenService jwtTokenService, ILogger logger, UserManager<InfiniLoreUser> userManager)
    : Endpoint<
        JwtCreateTokensRequest,
        Results<
            BadRequest<ProblemDetails>,
            Ok<JwtResponse>
        >
    > {
    public override void Configure() {
        Post("/account/jwt/token/create");
        AllowAnonymous();
    }

    public async override Task<Results<BadRequest<ProblemDetails>, Ok<JwtResponse>>> ExecuteAsync(JwtCreateTokensRequest req, CancellationToken ct) {
        try {
            UserIdentityResult signInResult = await apiSignInService.SignInAsync(req.Username, req.Password, ct).ConfigureAwait(false);

            switch (signInResult.Value) {
                case Error<string>:
                    logger.Warning("Sign-in failed for user {Username}. Error: {ErrorMessage}", req.Username, signInResult.ErrorString);
                    return TypedResults.BadRequest(new ProblemDetails { Detail = signInResult.ErrorString });
            }

            InfiniLoreUser user = signInResult.AsSuccess.Value;

            // Check user integrity before generating tokens
            if (!await userManager.Users.AnyAsync(u => u.NormalizedUserName == user.NormalizedUserName, ct)) {
                logger.Error("User with NormalizedUserName {NormalizedUserName} not found.", user.NormalizedUserName);
                return TypedResults.BadRequest(new ProblemDetails { Detail = "User not found." });
            }

            JwtResult jwtResult = await jwtTokenService.GenerateTokensAsync(user, req.Roles, req.Permissions, req.RefreshExpiresInDays, ct).ConfigureAwait(false);

            switch (jwtResult.Value) {
                case JwtTokenData data:
                    logger.Information("JWT tokens generated successfully for user {Username}.", req.Username);
                    return TypedResults.Ok(new JwtResponse(data.AccessToken, data.AccessTokenExpiryUtc, data.RefreshToken, data.RefreshTokenExpiryUtc));

                default:
                    logger.Warning("Token generation failed for user {Username}. Error: {ErrorMessage}", req.Username, jwtResult.IsError ? jwtResult.ErrorString : "Unknown error.");
                    return TypedResults.BadRequest(new ProblemDetails { Detail = "Unable to generate tokens." });
            }
        }
        catch (Exception ex) {
            logger.Error(ex, "An unexpected error occurred while processing JWT token creation for user {Username}", req.Username);
            return TypedResults.BadRequest(new ProblemDetails { Detail = "An unexpected error occurred." });
        }
    }
}
