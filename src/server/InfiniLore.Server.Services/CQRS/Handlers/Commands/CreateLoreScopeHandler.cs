// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data;
using InfiniLore.Server.Data.Models.UserData;
using InfiniLore.Server.Services.CQRS.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Serilog;

namespace InfiniLore.Server.Services.CQRS.Handlers.Commands;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CreateLoreScopeHandler(ILoreScopeRepository repository, IDbUnitOfWork<InfiniLoreDbContext> unitOfWork, ILogger logger) : IRequestHandler<CreateLoreScopeCommand, Union<Success<Guid>, Error<string>>> {
    public async Task<Union<Success<Guid>, Error<string>>> Handle(CreateLoreScopeCommand request, CancellationToken ct) {
        try {
            CommandResult<LoreScopeModel> result = await repository.TryAddWithResultAsync(request.Model, ct);
            if (!result.TryGetSuccessValue(out EntityEntry<LoreScopeModel>? loreScope)) {
                return new Error<string>($"Could not add lore scope: {result.ErrorString}");
            }
            
            await unitOfWork.CommitAsync(ct);
            return new Success<Guid>(loreScope.Entity.Id);
        }
        catch (Exception ex) {
            logger.Error(ex, "Error while creating LoreScope");
            return new Error<string>("Unexpected error while creating LoreScope");
        }
    }
}
