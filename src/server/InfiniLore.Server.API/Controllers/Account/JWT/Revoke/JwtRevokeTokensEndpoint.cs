// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Services;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using OneOf.Types;
using System.Security.Claims;

namespace InfiniLore.Server.API.Controllers.Account.JWT.Revoke;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class JwtRevokeTokensEndpoint(IJwtTokenService jwtTokenService, ILogger logger, UserManager<InfiniLoreUser> userManager)
    : Endpoint<
        JwtRevokeTokensRequest,
        Results<
            BadRequest<ProblemDetails>,
            Ok
        >
    > {
    public override void Configure() {
        Delete("/account/jwt/token/refresh");
        PermissionsAll("account.jwt.tokens_revoke");
    }
    public async override Task<Results<BadRequest<ProblemDetails>, Ok>> ExecuteAsync(JwtRevokeTokensRequest req, CancellationToken ct) {
        if (await userManager.FindByIdAsync(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value) is not {} user) {
            return TypedResults.BadRequest(new ProblemDetails { Detail = "User not found." });
        }

        List<ProblemDetails.Error> errors = [];
        foreach (Guid refreshToken in req.RefreshTokens) {
            TrueFalseOrError result = await jwtTokenService.RevokeTokensAsync(user, refreshToken, ct);
            switch (result.Value) {
                case True: {
                    continue;
                }

                case False: {
                    errors.Add(new ProblemDetails.Error {
                        Name = "Unable to revoke tokens.",
                        Reason = "Unable to revoke tokens."
                    });
                    break;
                }

                default: {
                    logger.Warning("Unable to revoke tokens for refreshToken {@Token}. Result: {@BoolResult}", refreshToken, result.ErrorString);
                    break;
                }
            }
        }

        if (errors.Count == 0) return TypedResults.Ok();
        return TypedResults.BadRequest(new ProblemDetails { Detail = "Unable to revoke tokens.", Errors = errors });
    }
}
