// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.API.Controllers.Account.Identity.CreateUser;
using InfiniLore.Server.Data.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace InfiniLore.Server.API.Controllers.Account.Identity.GetSpecificUser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetSpecificUserEndpoint(SignInManager<InfiniLoreUser> signInManager, ILogger logger)
    : Endpoint<
        GetSpecificUserRequest,
        Results<
            BadRequest<ProblemDetails>,
            Ok<UserResponse>
        >,
        UserResponseMapper
    > {


    public async override Task HandleAsync(GetSpecificUserRequest req, CancellationToken ct) {
        var problemDetails = new ProblemDetails {
            Detail = "Search yielded no result",
            Status = StatusCodes.Status400BadRequest
        };
        switch (req) {
            case { IsEmpty: true }: {
                await SendAsync(TypedResults.BadRequest(problemDetails), cancellation: ct);
                return;
            }

            case { UserId: {} userId, Username: null }
            case { UserId: null, Username: {} username }: {
                
            }
        }
        
        if (req.IsEmpty) {
            
        }
    }
}