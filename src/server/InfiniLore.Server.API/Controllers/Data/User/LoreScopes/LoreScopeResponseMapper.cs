// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Contracts.Data.Repositories;
using InfiniLore.Server.Contracts.Types.Results;
using InfiniLore.Server.Data.Models.Content.Account;
using InfiniLore.Server.Data.Models.Content.UserData;

namespace InfiniLore.Server.API.Controllers.Data.User.LoreScopes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class LoreScopeResponseMapper : Mapper<LoreScopeForm, LoreScopeResponse, LoreScopeModel> {
    public override LoreScopeResponse FromEntity(LoreScopeModel ls) => new(
        Id: ls.Id,
        UserId: ls.OwnerId,
        Name: ls.Name,
        Description: ls.Description,
        MultiverseIds: ls.Multiverses.Select(selector: m => m.Id).ToArray()
    );

    public async override Task<LoreScopeModel> ToEntityAsync(LoreScopeForm request, CancellationToken ct = new()) {
        var userRepository = Resolve<IUserRepository>();
        QueryResult<InfiniLoreUser> result = await userRepository.TryGetByIdAsync(request.UserId, ct);
        
        if (!result.TryGetSuccessValue(out InfiniLoreUser? user)) {
            throw new ArgumentException($"User not found with id {request.UserId}");
        }
        
        return new LoreScopeModel {
            Owner = user,
            Name = request.Name,
            Description = request.Description
        };
    }
}
