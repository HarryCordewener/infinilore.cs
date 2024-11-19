// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace InfiniLore.Server.Data.Models.Base;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class UserContent : BaseContent, IHasOwner {
    public bool IsPublic { get; set; }
    public ICollection<UserContentAccess> UserAccess { get; set; } = [];
    public required virtual InfiniLoreUser Owner { get; set; } = null!;
    [MaxLength(450)] public string OwnerId { get; set; } = null!;
}
