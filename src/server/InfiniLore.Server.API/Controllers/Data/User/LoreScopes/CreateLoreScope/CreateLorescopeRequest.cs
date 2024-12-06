// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Types.Unions;
using Microsoft.AspNetCore.Mvc;

namespace InfiniLore.Server.API.Controllers.Data.User.Lorescopes.CreateLorescope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record CreateLorescopeRequest(
    [FromRoute] UserIdUnion UserId,
    Guid LorescopeId,
    string Name,
    string? Description
);
