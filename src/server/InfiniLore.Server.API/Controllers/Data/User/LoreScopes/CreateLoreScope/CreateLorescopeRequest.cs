// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Unions;
using Microsoft.AspNetCore.Mvc;

namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes.CreateLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record CreateLorescopeRequest(
    [FromRoute] UserIdUnion UserId,
    Guid LoreScopeId,
    string Name,
    string? Description
);
