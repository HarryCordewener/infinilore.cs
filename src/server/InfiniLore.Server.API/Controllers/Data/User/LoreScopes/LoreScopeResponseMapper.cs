// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.Account;
using InfiniLore.Database.Models.Content.UserData;
using InfiniLore.Server.Contracts.Database.Repositories;
using InfiniLore.Server.Contracts.Types.Results;

namespace InfiniLore.Server.API.Controllers.Data.User.Lorescopes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class LorescopeResponseMapper : Mapper<LorescopeForm, LorescopeResponse, LorescopeModel> {
    public override LorescopeResponse FromEntity(LorescopeModel ls) => new(
        ls.Id,
        ls.OwnerId,
        ls.Name,
        ls.Description,
        ls.Multiverses.Select(selector: m => m.Id).ToArray()
    );

    public async override Task<LorescopeModel> ToEntityAsync(LorescopeForm request, CancellationToken ct = new()) {
        var userRepository = Resolve<IUserRepository>();
        RepoResult<InfiniLoreUser> result = await userRepository.TryGetByIdAsync(request.UserId, ct);

        if (!result.TryGetSuccessValue(out InfiniLoreUser? user)) {
            throw new ArgumentException($"User not found with id {request.UserId}");
        }

        return new LorescopeModel {
            Owner = user,
            Name = request.Name,
            Description = request.Description
        };
    }
}
