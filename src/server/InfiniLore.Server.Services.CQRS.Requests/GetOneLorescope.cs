// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using MediatR;

namespace InfiniLore.Server.Services.CQRS.Requests;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record GetOneLorescopeQuery(
    UserIdUnion UserIdUnion,
    Guid LorescopeId
) : IRequest<SuccessOrFailure<LoreScopeModel, string>>;
