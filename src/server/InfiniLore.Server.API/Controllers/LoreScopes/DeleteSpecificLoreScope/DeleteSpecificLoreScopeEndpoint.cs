// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories.Commands;
using InfiniLore.Server.Data;
using InfiniLore.Server.Data.Models.UserData;
using InfiniLoreLib.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InfiniLore.Server.API.Controllers.LoreScopes.DeleteSpecificLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DeleteSpecificLoreScopeEndpoint(IDbUnitOfWork<InfiniLoreDbContext> unitOfWork, IInfiniLoreUserCommands userRepository, ILoreScopesCommands loreScopeRepository) :
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

    public override async Task<Results<Ok, NotFound>> ExecuteAsync(DeleteSpecificLoreScopeRequest req, CancellationToken ct) {
        // TODO Move to a service
        await unitOfWork.BeginTransactionAsync(ct);

        ResultMany<LoreScopeModel> resultLoreScopes = await userRepository.GetLoreScopesAsync(req.UserId, ct);
        if (resultLoreScopes.IsFailure || resultLoreScopes.Values is null) {
            return TypedResults.NotFound();// Nothing to rollback
        }

        LoreScopeModel? scope = resultLoreScopes.Values.FirstOrDefault(x => x.Id == req.LoreScopeId);
        if (scope is null) {
            return TypedResults.NotFound();// Nothing to rollback
        }

        Result<bool> resultDelete = await loreScopeRepository.DeleteAsync(scope, ct);
        if (resultDelete.IsFailure) {
            await unitOfWork.RollbackAsync(ct);
            return TypedResults.NotFound();
        }

        if (await unitOfWork.TryCommitAsync(ct)) return TypedResults.Ok();

        await unitOfWork.RollbackAsync(ct);
        return TypedResults.NotFound();

    }
}
