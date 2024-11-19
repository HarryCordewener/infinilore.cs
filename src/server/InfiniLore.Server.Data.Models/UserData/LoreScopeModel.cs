// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniLore.Server.Data.Models.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoreScopeModel : UserContent {
    [MaxLength(64)] public required string Name { get; set; }
    [MaxLength(512)] public string Description { get; set; } = string.Empty;

    public ICollection<MultiverseModel> Multiverses { get; init; } = [];
    [NotMapped] public bool HasOnlyOneMultiverse => Multiverses.Count == 1;
    [NotMapped] public MultiverseModel? SingleMultiverse => HasOnlyOneMultiverse ? Multiverses.FirstOrDefault() : null; 
}
