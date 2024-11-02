// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.LoreScopes.GetAll;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetAllLoreScopesEndpoint(ILoreScopeQueries loreScopeQueries) :
    Endpoint<
        GetAllLoreScopesRequest,
        Results<
            Ok<IEnumerable<LoreScopeResponse>>,
            NotFound,
            ForbidHttpResult
        >,
        LoreScopeResponseMapper
    > {

    public override void Configure() {
        Get("/{UserId:guid}/lore-scopes/");
        // Permissions("read:lore-scopes");
        AllowAnonymous();
    }

    public async override Task<Results<Ok<IEnumerable<LoreScopeResponse>>, NotFound, ForbidHttpResult>> ExecuteAsync(GetAllLoreScopesRequest req, CancellationToken ct) {
        QueryOutputMany<LoreScopeModel> resultLoreScopes = await loreScopeQueries.TryGetByUserAsync(req.UserId, ct);
        if (!resultLoreScopes.TryGetSuccessValue(out LoreScopeModel[]? models)) {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(models.Select(ls => Map.FromEntity(ls)));
    }
}
