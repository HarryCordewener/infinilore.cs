// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.UserData;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using OneOf;
using OneOf.Types;
using NotFound=Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace InfiniLore.Server.API.Controllers.LoreScopes.CreateLoreScope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CreateLoreScopeEndpoint(IMediator mediator) :
    Endpoint<
        CreateLoreScopeRequest,
        Results<
            Ok,
            NotFound
        >,
        LoreScopeResponseMapper
    > {

    public override void Configure() {
        Post("/{UserId:guid}/lore-scopes/{LoreScopeId:guid}");
        AllowAnonymous();
    }

    // public override Task HandleAsync(CreateLoreScopeRequest req, CancellationToken ct) {
    //      mediator.
    // }

}

public record CreateLoreScopeCommand(Dictionary<string, string> data) : IRequest<OneOf<Success<LoreScopeModel>, Error>>;

public class CreateLoreScopeHandler : IRequestHandler<CreateLoreScopeCommand, OneOf<Success<LoreScopeModel>, Error>> {
    public async Task<OneOf<Success<LoreScopeModel>, Error>> Handle(CreateLoreScopeCommand request, CancellationToken cancellationToken) {
        return new Error();
    }
}

