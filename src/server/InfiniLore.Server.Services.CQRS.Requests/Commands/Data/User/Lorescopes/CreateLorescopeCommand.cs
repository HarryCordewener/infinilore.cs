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
public record CreateLorescopeCommand(
      HttpContext HttpContext,
      LoreScopeModel Lorescope  
) : ICqrsRequest<LoreScopeModel>;
