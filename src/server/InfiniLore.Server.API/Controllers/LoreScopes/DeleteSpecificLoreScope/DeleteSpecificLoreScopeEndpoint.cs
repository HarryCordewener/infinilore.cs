// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Contracts.Data.Repositories.Queries;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data;
using InfiniLore.Server.Data.Models.UserData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.LoreScopes.DeleteSpecificLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DeleteSpecificLoreScopeEndpoint(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork, ILoreScopeQueries loreScopeQueries, ILoreScopesCommands loreScopeCommands) :
    Endpoint<
        DeleteSpecificLoreScopeRequest,
        Results<
            Ok,
            NotFound
        >,
        LoreScopeResponseMapper
    > {

    public override void Configure() {
        Delete("/{UserId:guid}/lore-scopes/{LoreScopeId:guid}");
        AllowAnonymous();
    }


    public async override Task<Results<Ok, NotFound>> ExecuteAsync(DeleteSpecificLoreScopeRequest req, CancellationToken ct) {
        await unitOfWork.BeginTransactionAsync(ct);

        QueryOutput<LoreScopeModel> resultUser = await loreScopeQueries.TryGetByIdAsync(req.UserId, ct);
        if (!resultUser.TryGetSuccessValue(out LoreScopeModel? loreScope)) {
            return TypedResults.NotFound();// Fine for now
        }

        CommandOutput resultDelete = await loreScopeCommands.TryDeleteAsync(loreScope, ct);
        if (resultDelete.IsError) return TypedResults.NotFound();

        await unitOfWork.CommitAsync(ct);
        return TypedResults.Ok();
    }
}
