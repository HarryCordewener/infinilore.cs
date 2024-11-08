// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.API.Controllers.Account.Identity.CreateUser;
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Account.Identity.GetSpecificUser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetSpecificUserEndpoint(ILogger logger, IUserQueries queries)
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

            case { UserId: {} userId, Username: null }: {
                QueryOutput<InfiniLoreUser> result = await queries.TryGetByIdAsync(userId, ct);
                await SendResult(result,problemDetails, ct);
                return;
            }
            case { UserId: null, Username: {} username }: {
                QueryOutput<InfiniLoreUser> result = await queries.TryGetByUserNameAsync(username, ct);
                await SendResult(result,problemDetails, ct);
                return;
            }

            case { UserId : {} userId, Username: {} username }: {
                QueryOutputMany<InfiniLoreUser> result = await queries.TryGetByQueryAsync(user => user.Id == userId.ToString() && user.UserName == username, ct);
                if (!result.TryGetSuccessValue(out InfiniLoreUser[]? users)) {
                    await SendAsync(TypedResults.BadRequest(problemDetails), cancellation: ct);
                    return;
                }

                await SendAsync(TypedResults.Ok(await Map.FromEntityAsync(users.First(), ct)), cancellation:ct);
                return;
            }
        }
    }
    
    private async Task SendResult(QueryOutput<InfiniLoreUser> result, ProblemDetails problemDetails, CancellationToken ct) {
        await result.Match(
            // Success
            async success => {
                InfiniLoreUser user = success.Value;
                await SendAsync(TypedResults.Ok(await Map.FromEntityAsync(user, ct)), cancellation: ct);
            } ,
            // None
            async _ => await SendAsync(TypedResults.BadRequest(problemDetails), cancellation: ct),
                    
            // Error
            async _ => {
                logger.Error("Unexpected error during retrieval of User, with {error}", result.ErrorString);
                await SendAsync(TypedResults.BadRequest(problemDetails), cancellation: ct);
            }
        );
    }
}