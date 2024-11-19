// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;

namespace InfiniLore.Server.API.Controllers.Account.Identity.GetSpecificUser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record GetSpecificUserRequest(
    [FromQuery] string? Username,
    [FromQuery] Guid? UserId
) {
    public bool IsEmpty => Username is null && UserId is null;
}
