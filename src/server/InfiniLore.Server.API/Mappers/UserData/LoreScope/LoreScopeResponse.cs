// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.API.Mappers.Base;

namespace InfiniLore.Server.API.Mappers.UserData.LoreScope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record LoreScopeResponse(
    Guid id, 
    DateTime createdDate,
    string ownerId, 
    string Name,
    string Description,
    ICollection<Guid> MultiverseIds
    ) : UserContentResponse(
    id,
    createdDate,
    ownerId
);
