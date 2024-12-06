// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.Contracts.Services.CQRS;
using InfiniLore.Server.Contracts.Types.Unions;
using MediatR;

// ReSharper disable once CheckNamespace
namespace InfiniLore.Server.Services.CQRS.Requests.Queries;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record GetOneLorescopeQuery(
    UserIdUnion UserIdUnion,
    Guid LorescopeId) : ICqrsRequest<LorescopeModel>;
