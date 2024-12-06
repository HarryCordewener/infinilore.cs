// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.Services.CQRS.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Data.User.Lorescopes.CreateLorescope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
using EndpointResult=Results<Ok<LorescopeResponse>, BadRequest<ProblemDetails>>;

public class CreateLorescopeEndpoint(IMediator mediator) : Endpoint<LorescopeForm, EndpointResult, LorescopeResponseMapper> {
    public override void Configure() {
        Post("/data-user/{UserId:guid}/lore-scopes/");
        // Permissions("data-user.lore-scopes.create");
    }

    public async override Task<EndpointResult> ExecuteAsync(LorescopeForm req, CancellationToken ct) {
        SuccessOrFailure<LorescopeModel> result = await mediator.Send(
            new CreateLorescopeCommand(
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
