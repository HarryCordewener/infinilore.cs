// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Services.Auth.Authorization;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Services.CQRS.Requests.Queries;
using MediatR;
using Serilog;

namespace InfiniLore.Server.Services.CQRS.Handlers.Queries;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetOneLoreScopeHandler(ILoreScopeRepository loreScopeRepository, ILogger logger, IUserContentAuthorizationService authService) : IRequestHandler<GetOneLorescopeQuery, SuccessOrFailure<LoreScopeModel, string>> {

    public async Task<SuccessOrFailure<LoreScopeModel, string>> Handle(GetOneLorescopeQuery request, CancellationToken ct) {
        try {
            if (!await authService.ValidateAsync(request.LorescopeId, AccessKind.Read, ct)) return "Access Denied";

            // After everything is validated, we can finally store to the db
            RepoResult<LoreScopeModel[]> result = await loreScopeRepository.TryGetByUserIdAndLorescopeId(request.UserIdUnion, request.LorescopeId, ct);

            return result.Value switch {
                Success<LoreScopeModel> success => success,
                Failure<string> failure => failure,// Pass failure directly
                _ => "An unknown error occurred"
            };
        }
        catch (Exception e) {
            logger.Error(e, "An error occurred while trying to get a lore scope");
            return "An unknown error occurred";
        }
    }
}
