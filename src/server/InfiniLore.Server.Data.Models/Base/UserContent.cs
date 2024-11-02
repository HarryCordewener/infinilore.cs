// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;

namespace InfiniLore.Server.Data.Models.Base;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class UserContent<T> : BaseContent<T>, IHasOwner
    where T : BaseContent<T> {

    public bool IsPublic { get; set; }
    public ICollection<UserContentAccess<T>> UserAccess { get; set; } = [];
    public required virtual InfiniLoreUser Owner { get; set; }
    public string OwnerId { get; set; } = null!;
}
