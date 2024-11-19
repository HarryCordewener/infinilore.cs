// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;

namespace InfiniLore.Server.Data.Models.Base;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class UserContent : BaseContent, IHasOwner {
    public bool IsPublic { get; set; }
    public ICollection<UserContentAccess> UserAccess { get; set; } = [];
    public virtual InfiniLoreUser Owner { get; set; } = null!;
    public required string OwnerId { get; set; }
}
