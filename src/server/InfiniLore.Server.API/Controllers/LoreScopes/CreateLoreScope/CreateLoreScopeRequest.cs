// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;

namespace InfiniLore.Server.API.Controllers.LoreScopes.CreateLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public record CreateLoreScopeRequest(
    [property: FromRoute] Guid UserId,
    Guid LoreScopeId,
    string OwnerId,
    string Name,
    string? Description
);
