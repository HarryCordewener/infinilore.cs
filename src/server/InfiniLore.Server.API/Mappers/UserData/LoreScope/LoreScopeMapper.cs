// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.UserData;

namespace InfiniLore.Server.API.Mappers.UserData.LoreScope;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoreScopeMapper() : Mapper<LoreScopeRequest, LoreScopeResponse, LoreScopeModel> {
    
    public override LoreScopeResponse FromEntity(LoreScopeModel model) => new(
        id:model.Id,
        createdDate:model.CreatedDate,
        ownerId:model.OwnerId,
        Name:model.Name,
        Description:model.Description,
        MultiverseIds: model.Multiverses.Select(m => m.Id).ToArray()
    );
    
    public override LoreScopeModel ToEntity(LoreScopeRequest request) => new() {
        Id = request.Id ?? Guid.CreateVersion7(),
        OwnerId = request.OwnerId,
        Name = request.Name,
        Description = request.Description ?? string.Empty
    };
}
