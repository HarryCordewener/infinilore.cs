// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.UserData;

namespace InfiniLore.Server.API.Mappers.UserData.LoreScope;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoreScopeMapper : Mapper<LoreScopeRequest, LoreScopeResponse, LoreScopeModel> {

    public override LoreScopeResponse FromEntity(LoreScopeModel model) => new(
        model.Id,
        model.CreatedDate,
        model.OwnerId,
        model.Name,
        model.Description,
        model.Multiverses.Select(m => m.Id).ToArray()
    );

    public override LoreScopeModel ToEntity(LoreScopeRequest request) => new() {
        Id = request.Id ?? Guid.CreateVersion7(),
        Owner = null!,
        OwnerId = request.OwnerId,
        Name = request.Name,
        Description = request.Description ?? string.Empty
    };
}
