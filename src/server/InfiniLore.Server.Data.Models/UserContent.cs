// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.Account;
using System.ComponentModel.DataAnnotations;

namespace InfiniLore.Server.Data.Models;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class UserContent : BaseContent, IHasOwner {
    public required virtual InfiniLoreUser Owner { get; set; } = null!;
    [MaxLength(450)] public string OwnerId { get; set; } = null!;
    
    public ICollection<UserContentAccessModel> UserAccess { get; set; } = [];
    public bool IsPubliclyReadable { get; set; } // When authorization validation happens, this should overwrite the user's access level to always allow for read access
    public bool IsDiscoverable { get; set; } = true;
}
