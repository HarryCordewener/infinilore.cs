// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Unions;
using Microsoft.AspNetCore.Mvc;

namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes.GetLoreScope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record GetLorescopeRequest(
    [FromRoute] UserIdUnion UserId, 
    [FromRoute] Guid LoreScopeId
);
