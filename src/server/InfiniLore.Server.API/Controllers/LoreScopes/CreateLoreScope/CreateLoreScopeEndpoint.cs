// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.API.Mappers.UserData.LoreScope;
using InfiniLore.Server.Data.Models.UserData;
using InfiniLore.Server.Services.CQRS.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;


namespace InfiniLore.Server.API.Controllers.LoreScopes.CreateLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CreateLoreScopeEndpoint(IMediator mediator) :
    Endpoint<
        LoreScopeRequest, // Uses the generic request type to map the request to the entity model, aka no special request class needed.
        Results<
            Ok<Guid>,
            BadRequest
        >,
        LoreScopeMapper
    > {

    public override void Configure() {
        Post("/{UserId:guid}/lore-scopes");
        AllowAnonymous();
    }

    public async override Task HandleAsync(LoreScopeRequest req, CancellationToken ct) {
        LoreScopeModel model = Map.ToEntity(req);
        Union<Success<Guid>, Error<string>> result = await mediator.Send(new CreateLoreScopeCommand(model), ct);
        
        if (!result.IsT0) {
            await SendAsync(TypedResults.BadRequest(), cancellation: ct);
            return;
        }
        await SendAsync(TypedResults.Ok(result.AsT0.Value), cancellation: ct);
    }
}