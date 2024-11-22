// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Services.CQRS.Requests;
using MediatR;
using Serilog;

namespace InfiniLore.Server.Services.CQRS.Handlers.Queries;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetOneLoreScopeHandler(ILoreScopeRepository loreScopeRepository, ILogger logger) : IRequestHandler<GetOneLorescopeQuery, SuccessOrFailure<LoreScopeModel, string>> {

    public async Task<SuccessOrFailure<LoreScopeModel, string>> Handle(GetOneLorescopeQuery request, CancellationToken ct) {
        try {
            QueryResultMany<LoreScopeModel> result = await loreScopeRepository.TryGetByUserIdAndLorescopeId(request.UserIdUnion, request.LorescopeId, ct);
       
            return result.Value switch {
                Success<LoreScopeModel> success => success,
                None => "No lore scopes were found for the provided user and lorescope id",
                Error<string> error => error.Value,
                _ => "An unknown error occurred"
            };
        }
        catch (Exception e) {
            logger.Error(e, "An error occurred while trying to get a lore scope");
            return "An unknown error occurred";
        }
    }
}
