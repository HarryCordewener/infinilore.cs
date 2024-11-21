// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Services.CQRS.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
using EndpointResult=Results<Ok<LoreScopeResponse>, BadRequest<ProblemDetails>>;

public class GetLoreScopeEndpoint(IMediator mediator) : Endpoint<GetLorescopeRequest, EndpointResult> {

    public override void Configure() {
        Get("/data-user/{UserId:guid}/lore-scopes/{LoreScopeId:guid}");
        // Permissions("data-user.lore-scopes.read");
    }

    public async override Task<EndpointResult> ExecuteAsync(GetLorescopeRequest req, CancellationToken ct) {
        var data = await mediator.Send(
            GetLoreScopesQuery.FromOneLoreScope(req.UserId, req.LoreScopeId),
            ct
        );

        if (data.TryGetAsFailure(out Failure<string> failure)) {
            return TypedResults.BadRequest(new ProblemDetails {
                Detail = failure.Value
            });
        }

        return TypedResults.Ok<LoreScopeResponse>(
            data.AsSuccess.Value);
    }
}
