// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Services.CQRS;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;

// ReSharper disable once CheckNamespace
namespace InfiniLore.Server.Services.CQRS.Requests.Queries;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record GetOneLorescopeQuery(
    UserIdUnion UserIdUnion,
    Guid LorescopeId) : ICqrsRequest<LoreScopeModel>;
