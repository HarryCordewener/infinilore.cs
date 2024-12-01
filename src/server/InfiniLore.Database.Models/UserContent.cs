// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.Account;

namespace InfiniLore.Database.Models;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class UserContent : BaseContent, IHasOwner {

    public ICollection<UserContentAccessModel> UserAccess { get; set; } = [];
    public bool IsPubliclyReadable { get; set; }// When authorization validation happens, this should overwrite the user's access level to always allow for read access
    public bool IsDiscoverable { get; set; } = true;
    public required virtual InfiniLoreUser Owner { get; set; } = null!;
    public Guid OwnerId { get; set; }
}
