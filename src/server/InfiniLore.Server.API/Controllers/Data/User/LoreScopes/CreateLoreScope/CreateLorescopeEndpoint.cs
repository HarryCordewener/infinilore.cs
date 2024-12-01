// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Services.CQRS.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes.CreateLoreScope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
using EndpointResult=Results<Ok<LoreScopeResponse>, BadRequest<ProblemDetails>>;

public class CreateLorescopeEndpoint(IMediator mediator) : Endpoint<LoreScopeForm, EndpointResult, LoreScopeResponseMapper> {
    public override void Configure() {
        Post("/data-user/{UserId:guid}/lore-scopes/");
        // Permissions("data-user.lore-scopes.create");
    }

    public async override Task<EndpointResult> ExecuteAsync(LoreScopeForm req, CancellationToken ct) {
        SuccessOrFailure<LoreScopeModel, string> result = await mediator.Send(
            new CreateLorescopeCommand(
                HttpContext,
                await Map.ToEntityAsync(req, ct)),
            ct
        );

        if (result.TryGetAsFailure(out Failure<string> failure)) {
            return TypedResults.BadRequest(new ProblemDetails {
                Detail = failure.Value
            });
        }

        return TypedResults.Ok(Map.FromEntity(result.AsSuccess.Value));
    }
}
