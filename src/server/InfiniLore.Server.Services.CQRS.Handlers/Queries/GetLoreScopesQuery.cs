// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Data.Models.Content.UserData;
using InfiniLore.Server.Services.CQRS.Requests;
using MediatR;

namespace InfiniLore.Server.Services.CQRS.Handlers.Queries;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GetLoreScopesHandler(ILoreScopeRepository loreScopeRepository) : IRequestHandler<GetLoreScopesQuery, SuccessOrFailure<LoreScopeModel, string>> {

    public async Task<SuccessOrFailure<LoreScopeModel, string>> Handle(GetLoreScopesQuery request, CancellationToken cancellationToken) {
        if (request.IsEmpty) return "No query parameters were provided";
    }
}
