// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

// ReSharper disable NotAccessedPositionalProperty.Global
public record LoreScopeResponse(
    Guid Id,
    string UserId,
    string Name,
    string Description,
    ICollection<Guid> MultiverseIds
);
