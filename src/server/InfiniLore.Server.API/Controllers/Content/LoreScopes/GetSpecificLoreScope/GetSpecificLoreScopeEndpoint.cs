// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.API.Controllers.Data.User.LoreScopes;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.LoreScopes.GetSpecificLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetSpecificLoreScopeEndpoint(ILoreScopeRepository loreScopeQueries) :
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
        QueryResult<LoreScopeModel> resultLoreScope = await loreScopeQueries.TryGetByIdAsync(req.LoreScopeId, ct);
        return resultLoreScope.Match<Results<Ok<LoreScopeResponse>, NotFound>>(
            successCase: success => TypedResults.Ok(Map.FromEntity(success.Value)),
            noneCase: _ => TypedResults.NotFound(),
            errorCase: _ => TypedResults.NotFound()
        );
    }
}
