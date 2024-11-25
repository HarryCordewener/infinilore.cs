// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using AterraEngine.Unions;
using InfiniLore.Server.Data.Models.Content.UserData;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InfiniLore.Server.Services.CQRS.Requests.Commands;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record CreateLorescopeCommand(
      HttpContext HttpContext,
      LoreScopeModel Lorescope  
) : IRequest<SuccessOrFailure<LoreScopeModel, string>>;
