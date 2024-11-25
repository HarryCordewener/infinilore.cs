// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.API.Controllers.Data.User.LoreScopes;
using InfiniLore.Server.API.Controllers.LoreScopes.DeleteSpecificLoreScope;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Data.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.Content.LoreScopes.DeleteSpecificLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DeleteSpecificLoreScopeEndpoint(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork, ILoreScopeRepository repository) :
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

        QueryResult<LoreScopeModel> resultUser = await repository.TryGetByIdAsync(req.UserId, ct);
        if (!resultUser.TryGetSuccessValue(out LoreScopeModel? loreScope)) {
            return TypedResults.NotFound();// Fine for now
        }

        CommandOutput resultDelete = await repository.TryDeleteAsync(loreScope, ct);
        if (resultDelete.IsFailure) return TypedResults.NotFound();

        await unitOfWork.CommitAsync(ct);
        return TypedResults.Ok();
    }
}
