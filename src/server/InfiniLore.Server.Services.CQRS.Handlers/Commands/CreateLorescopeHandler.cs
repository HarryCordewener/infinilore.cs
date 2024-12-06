// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Database.MsSqlServer;
using InfiniLore.Server.Contracts.Database;
using InfiniLore.Server.Contracts.Database.Repositories.Content.Data.User;
using InfiniLore.Server.Contracts.Services.Auth.Authorization;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Services.CQRS.Requests.Commands;
using MediatR;
using Serilog;

namespace InfiniLore.Server.Services.CQRS.Handlers.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CreateLorescopeHandler(ILoreScopeRepository loreScopeRepository, ILogger logger, IDbUnitOfWork<MsSqlDbContext> unitOfWork, IUserContentAuthorizationService authService) : IRequestHandler<CreateLorescopeCommand, SuccessOrFailure<LoreScopeModel>> {
    public async Task<SuccessOrFailure<LoreScopeModel>> Handle(CreateLorescopeCommand request, CancellationToken ct) {
        try {
            if (!await authService.ValidateIsOwnerAsync(request.Lorescope.OwnerId, ct)) return "Access Denied";
            
            RepoResult resultCanUseName = await loreScopeRepository.CanUseAsNewLorescopeNameAsync(request.Lorescope.OwnerId, request.Lorescope.Name, ct);
            if (resultCanUseName.IsFailure) return resultCanUseName.AsFailure;

            RepoResult<LoreScopeModel> result = await loreScopeRepository.TryAddWithResultAsync(request.Lorescope, ct);
            await unitOfWork.CommitAsync(ct);

            return result.Value switch {
                Success<LoreScopeModel> success => success,
                Failure<string> failure => failure, // Pass failure directly
                _ => throw new ArgumentException("Result union did not have a valid success or failure value")
            };
        }
        catch (Exception e) {
            logger.Error(e, "An error occurred while trying to create a lore scope");
            return "An unknown error occurred";
        }
    }
}
