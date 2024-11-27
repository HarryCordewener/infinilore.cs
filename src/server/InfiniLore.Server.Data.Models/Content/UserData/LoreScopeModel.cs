// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.Server.Data.Models.Content.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoreScopeModel : UserContent {
    [MaxLength(64)] public required string Name { get; set; }
    [MaxLength(512)] public string Description { get; set; } = string.Empty;

    public ICollection<MultiverseModel> Multiverses { get; init; } = [];

    [NotMapped] public bool HasOnlyOneMultiverse => Multiverses.Count == 1;
    [NotMapped] [MemberNotNull(nameof(HasOnlyOneMultiverse))] public MultiverseModel? SingleMultiverse
        => HasOnlyOneMultiverse == false ? null : Multiverses.First();
}
