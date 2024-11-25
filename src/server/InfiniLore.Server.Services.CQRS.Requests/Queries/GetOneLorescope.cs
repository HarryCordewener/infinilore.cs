// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InfiniLore.Server.Services.CQRS.Requests.Queries;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record GetOneLorescopeQuery(
    HttpContext HttpContext,
    UserIdUnion UserIdUnion,
    Guid LorescopeId) : IRequest<SuccessOrFailure<LoreScopeModel, string>>;
