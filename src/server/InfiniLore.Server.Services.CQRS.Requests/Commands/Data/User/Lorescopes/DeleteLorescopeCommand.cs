// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Services.CQRS;
using InfiniLore.Server.Data.Models.Content.UserData;
using Microsoft.AspNetCore.Http;

// ReSharper disable once CheckNamespace
namespace InfiniLore.Server.Services.CQRS.Requests.Commands;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record DeleteLorescopeCommand(
    HttpContext HttpContext,
    Guid LorescopeId
) : ICqrsRequest<LoreScopeModel>;
