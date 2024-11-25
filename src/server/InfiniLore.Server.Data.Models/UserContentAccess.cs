// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.Account;
using System.ComponentModel.DataAnnotations;

namespace InfiniLore.Server.Data.Models;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UserContentAccessModel {
    [Key] public Guid Id { get; init; } = Guid.CreateVersion7(); // Not part of the BaseContent family

    public virtual object? Content { get; init; }
    public Guid ContentId { get; init; }
    
    public required InfiniLoreUser User { get; init; }
    [MaxLength(450)] public string UserId { get; init; } = null!;
    
    public required AccessKind AccessKind { get; init; }
}

[Flags]
public enum AccessKind : ulong {
    Undefined = 0,
    Owner = 1 << 0 | Manage,
    Read = 1 << 1,
    Write = 1 << 2,
    Delete = 1 << 3,
    Manage = Read | Write | Delete ,
}
