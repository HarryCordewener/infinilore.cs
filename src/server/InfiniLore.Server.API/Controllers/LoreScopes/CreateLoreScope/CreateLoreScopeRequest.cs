// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.UserData;
using Microsoft.AspNetCore.Mvc;

namespace InfiniLore.Server.API.Controllers.LoreScopes.CreateLoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public record CreateLoreScopeRequest(
    [property: FromRoute] Guid UserId,
    [property: FromRoute] Guid LoreScopeId, 
    [property: FastEndpoints.FromBody] LoreScopeModel Model
);
