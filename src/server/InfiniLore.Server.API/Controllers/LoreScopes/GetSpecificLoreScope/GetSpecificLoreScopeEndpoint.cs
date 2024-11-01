// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Data.Models.UserData;
using InfiniLoreLib.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.LoreScopes.GetSpecificLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetSpecificLoreScopeEndpoint(ILoreScopeQueries loreScopeQueries) :
    Endpoint<
        GetSpecificLoreScopeRequest,
        Results<
            Ok<LoreScopeResponse>,
            NotFound
        >,
        LoreScopeResponseMapper
    > {

    public override void Configure() {
        Get("/{UserId:guid}/lore-scopes/{LoreScopeId:guid}");
        AllowAnonymous();
    }

    public async override Task<Results<Ok<LoreScopeResponse>, NotFound>> ExecuteAsync(GetSpecificLoreScopeRequest req, CancellationToken ct) {
        QueryOutput<LoreScopeModel> resultLoreScope = await loreScopeQueries.TryGetByIdAsync(req.LoreScopeId, ct);
        return resultLoreScope.Match<Results<Ok<LoreScopeResponse>, NotFound>>(
            success => TypedResults.Ok(Map.FromEntity(success.Value)),
            _ => TypedResults.NotFound(),
            _ => TypedResults.NotFound()
        );
    }
}
