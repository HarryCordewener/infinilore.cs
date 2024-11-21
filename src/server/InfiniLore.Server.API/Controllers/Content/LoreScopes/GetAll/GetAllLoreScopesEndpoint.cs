// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using FastEndpoints.Security;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models;
using InfiniLore.Server.Data.Models.Content.Account;
using InfiniLore.Server.Data.Models.Content.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace InfiniLore.Server.API.Controllers.Content.LoreScopes.GetAll;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetAllLoreScopesEndpoint(ILoreScopeRepository loreScopeQueries, UserManager<InfiniLoreUser> userManager) :
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
        Policy(x => x.RequireAssertion(context =>
            context.User.IsInRole("admin")
            || context.User.HasPermission("read:lore-scopes")
        ));
    }

    public async override Task<Results<Ok<IEnumerable<LoreScopeResponse>>, NotFound, ForbidHttpResult>> ExecuteAsync(GetAllLoreScopesRequest req, CancellationToken ct) {
        InfiniLoreUser? user = await userManager.GetUserAsync(HttpContext.User);
        if (user is null) return TypedResults.NotFound();

        QueryResultMany<LoreScopeModel> resultLoreScopes;
        LoreScopeModel[]? models;
        if (HttpContext.User.IsInRole("admin")) {
            resultLoreScopes = await loreScopeQueries.TryGetByUserAsync(req.UserId, ct);
            if (!resultLoreScopes.TryGetSuccessValue(out models)) return TypedResults.NotFound();

            return TypedResults.Ok(models.Select(ls => Map.FromEntity(ls)));
        }

        resultLoreScopes = await loreScopeQueries.TryGetByUserWithUserAccessAsync(req.UserId, user, AccessLevel.Read, ct);
        if (!resultLoreScopes.TryGetSuccessValue(out models)) return TypedResults.NotFound();

        IEnumerable<LoreScopeModel> data = models
            .Where(model => model.UserAccess.Any(access =>
                    access.User.Id == req.UserId.ToString()
                    && access.AccessLevel == AccessLevel.Read
                )
            );

        return TypedResults.Ok(data.Select(ls => Map.FromEntity(ls)));
    }
}
