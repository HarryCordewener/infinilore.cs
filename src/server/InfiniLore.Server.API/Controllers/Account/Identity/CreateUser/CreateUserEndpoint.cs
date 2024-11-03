// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace InfiniLore.Server.API.Controllers.Account.Identity.CreateUser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CreateUserEndpoint(SignInManager<InfiniLoreUser> signInManager, ILogger logger)
    : Endpoint<
        CreateUserRequest,
        Results<
            BadRequest<ProblemDetails>,
            Ok<UserResponse>
        >,
        UserResponseMapper
    > {

    public override void Configure() {
        Post("/account/identity/create");
        AllowAnonymous();
    }

    public async override Task<Results<BadRequest<ProblemDetails>, Ok<UserResponse>>> ExecuteAsync(CreateUserRequest req, CancellationToken ct) {
        try {
            var user = new InfiniLoreUser {
                UserName = req.UserName,
                Email = req.Email
            };

            IdentityResult result = await signInManager.UserManager.CreateAsync(user, req.Password);

            if (!result.Succeeded) {
                var problemDetails = new ProblemDetails {
                    Detail = "User Creation Failed" + string.Join("; ", result.Errors.Select(e => e.Description)),
                    Status = StatusCodes.Status400BadRequest
                };

                return TypedResults.BadRequest(problemDetails);
            }

            await signInManager.SignInAsync(user, false);
            return TypedResults.Ok(Map.FromEntity(user));
        }
        catch (Exception ex) {
            logger.Error(ex, "Error creating user");
            var problemDetails = new ProblemDetails {
                Detail = "An unexpected error occurred while creating the user.",
                Status = StatusCodes.Status400BadRequest
            };

            return TypedResults.BadRequest(problemDetails);
        }
    }
}
