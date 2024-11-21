// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Services;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace InfiniLore.Server.API.Controllers.Account.JWT.Revoke;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class JwtRevokeAllTokensEndpoint(IJwtTokenService jwtTokenService, ILogger logger, UserManager<InfiniLoreUser> userManager)
    : EndpointWithoutRequest<
        Results<
            BadRequest<ProblemDetails>,
            Ok
        >
    > {

    public override void Configure() {
        Delete("/account/jwt/token/refresh/all");
        PermissionsAll("account.jwt.tokens_revoke");
    }

    public async override Task<Results<BadRequest<ProblemDetails>, Ok>> ExecuteAsync(CancellationToken ct) {
        if (await userManager.GetUserAsync(User) is not {} user) {
            return TypedResults.BadRequest(new ProblemDetails { Detail = "User not found." });
        }

        TrueFalseOrError result = await jwtTokenService.RevokeAllTokensFromUserAsync(user, ct);
        switch (result.Value) {
            case True: {
                return TypedResults.Ok();
            }

            case False: {
                return TypedResults.BadRequest(new ProblemDetails { Detail = "Tokens could not be revoked." });
            }

            default: {
                logger.Warning("Unable to revoke tokens. Result: {@Error}", result.ErrorString);
                return TypedResults.BadRequest(new ProblemDetails { Detail = "Tokens could not be revoked." });
            }
        }
    }
}
