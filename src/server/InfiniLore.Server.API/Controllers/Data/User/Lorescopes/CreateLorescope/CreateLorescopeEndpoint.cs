// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.API.Controllers.Data.User.Lorescopes.CreateLoreScope;
using InfiniLore.Server.Contracts.Services;
using InfiniLore.Server.Services.CQRS.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Data.User.Lorescopes.CreateLorescope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CreateLorescopeEndpoint(IMediator mediator, IMediatorOutputService mediatorOutput) 
    : Endpoint<
        CreateLorescopeRequest,
        Results<Ok<LorescopeResponse>, BadRequest<ProblemDetails>>,
        CreateLorescopeMapper
    > {
    
    public override void Configure() {
        Post("/data-user/{UserId:guid}/lore-scopes/");
        AllowAnonymous();
        // Permissions("data-user.lore-scopes.create");
    }

    public async override Task<Results<Ok<LorescopeResponse>, BadRequest<ProblemDetails>>> ExecuteAsync(CreateLorescopeRequest req, CancellationToken ct) {
        SuccessOrFailure<LorescopeModel> result = await mediator.Send(
            new CreateLorescopeCommand(
                await Map.ToEntityAsync(req, ct)),
            ct
        );
        
        return mediatorOutput.ToHttpResults(result, Map.FromEntity);
    }
}
