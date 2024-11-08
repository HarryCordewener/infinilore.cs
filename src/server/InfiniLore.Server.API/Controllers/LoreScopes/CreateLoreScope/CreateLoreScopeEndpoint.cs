// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Services.CQRS.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using OneOf;
using OneOf.Types;

namespace InfiniLore.Server.API.Controllers.LoreScopes.CreateLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CreateLoreScopeEndpoint(IMediator mediator) :
    Endpoint<
        CreateLoreScopeRequest,
        Results<
            Ok<Guid>,
            BadRequest
        >,
        LoreScopeResponseMapper
    > {

    public override void Configure() {
        Post("/{UserId:guid}/lore-scopes/{LoreScopeId:guid}");
        AllowAnonymous();
    }

    public async override Task HandleAsync(CreateLoreScopeRequest req, CancellationToken ct) {
        OneOf<Success<Guid>, Error<string>> result = await mediator.Send(new CreateLoreScopeCommand(req.Model), ct);
        if (!result.IsT0) {
            await SendAsync(TypedResults.BadRequest(), cancellation: ct);
            return;
        }
        await SendAsync(TypedResults.Ok(result.AsT0.Value), cancellation: ct);
    }
}