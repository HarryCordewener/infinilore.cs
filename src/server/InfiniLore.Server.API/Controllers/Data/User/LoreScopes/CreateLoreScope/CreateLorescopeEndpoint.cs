// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes.CreateLoreScope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
using EndpointResult=Results<Ok<LoreScopeResponse>, BadRequest<ProblemDetails>>;

public class CreateLorescopeEndpoint(IMediator mediator) : Endpoint<CreateLorescopeRequest, EndpointResult, LoreScopeResponseMapper> {
    public override void Configure() {
        Post("/data-user/{UserId:guid}/lore-scopes/");
        // Permissions("data-user.lore-scopes.create");
    }

    public override Task<EndpointResult> ExecuteAsync(CreateLorescopeRequest req, CancellationToken ct) {
        
        // Todo implement CreateLorescope endpoint
        throw new NotImplementedException();
    }
}
