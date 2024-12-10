// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Server.API.Controllers.Data.User.Lorescopes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable NotAccessedPositionalProperty.Global
public record LorescopeResponse(
    Guid Id,
    Guid UserId,
    string Name,
    string Description,
    ICollection<Guid> MultiverseIds
);
