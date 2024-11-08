// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;

namespace InfiniLore.Server.API.Models;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class UserResponseMapper : ResponseMapper<UserResponse, InfiniLoreUser> {
    public override UserResponse FromEntity(InfiniLoreUser user) => new(
        Guid.Parse(user.Id),
        user.UserName ?? "UNDEFINED"
    );
}
