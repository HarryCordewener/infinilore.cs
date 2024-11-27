// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Services.CQRS.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes.GetLoreScope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
using EndpointResult=Results<Ok<LoreScopeResponse>, BadRequest<ProblemDetails>>;

public class GetLoreScopeEndpoint(IMediator mediator) : Endpoint<GetLorescopeRequest, EndpointResult, LoreScopeResponseMapper> {

    public override void Configure() {
        Get("/data-user/{UserId:guid}/lore-scopes/{LoreScopeId:guid}");
        // Permissions("data-user.lore-scopes.read");
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public async override Task<EndpointResult> ExecuteAsync(GetLorescopeRequest req, CancellationToken ct) {
        SuccessOrFailure<LoreScopeModel, string> data = await mediator.Send(
            new GetOneLorescopeQuery(
                req.UserId, 
                req.LoreScopeId
            ),
            ct
        );

        if (data.TryGetAsFailure(out Failure<string> failure)) {
            return TypedResults.BadRequest(new ProblemDetails {
                Detail = failure.Value
            });
        }

        return TypedResults.Ok(Map.FromEntity(data.AsSuccess.Value));
    }
}
