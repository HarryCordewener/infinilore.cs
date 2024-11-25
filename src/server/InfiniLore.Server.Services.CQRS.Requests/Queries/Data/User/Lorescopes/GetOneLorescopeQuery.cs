// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Services.CQRS;
using InfiniLore.Server.Contracts.Types.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace InfiniLore.Server.Services.CQRS.Requests.Queries;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record GetOneLorescopeQuery(
    HttpContext HttpContext,
    UserIdUnion UserIdUnion,
    Guid LorescopeId) : ICqrsRequest<LoreScopeModel>;
