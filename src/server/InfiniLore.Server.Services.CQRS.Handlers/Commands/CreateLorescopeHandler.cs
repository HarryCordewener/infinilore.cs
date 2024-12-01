// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Data;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Services.Auth.Authorization;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Data.SqlServer;
using InfiniLore.Server.Services.CQRS.Requests.Commands;
using MediatR;
using Serilog;

namespace InfiniLore.Server.Services.CQRS.Handlers.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CreateLorescopeHandler(ILoreScopeRepository loreScopeRepository, ILogger logger, IDbUnitOfWork<InfiniLoreDbContext> unitOfWork, IUserContentAuthorizationService authService) : IRequestHandler<CreateLorescopeCommand, SuccessOrFailure<LoreScopeModel, string>> {
    public async Task<SuccessOrFailure<LoreScopeModel, string>> Handle(CreateLorescopeCommand request, CancellationToken ct) {
        try {
            if (!await authService.ValidateIsOwnerAsync(request.Lorescope.OwnerId, ct)) return "Access Denied";

            RepoResult<LoreScopeModel> result = await loreScopeRepository.TryAddWithResultAsync(request.Lorescope, ct);
            await unitOfWork.CommitAsync(ct);

            return result.Value switch {
                Success<LoreScopeModel> success => success,
                Failure<string> failure => failure,// Pass failure directly
                _ => throw new ArgumentException("Result union did not have a valid success or failure value")
            };
        }
        catch (Exception e) {
            logger.Error(e, "An error occurred while trying to create a lore scope");
            return "An unknown error occurred";
        }
    }
}
