// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.UserData;

namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class LoreScopeResponseMapper : ResponseMapper<LoreScopeResponse, LoreScopeModel> {
    public override LoreScopeResponse FromEntity(LoreScopeModel ls) => new(
        Id: ls.Id,
        UserId: ls.OwnerId,
        Name: ls.Name,
        Description: ls.Description,
        MultiverseIds: ls.Multiverses.Select(selector: m => m.Id).ToArray()
    );
}
