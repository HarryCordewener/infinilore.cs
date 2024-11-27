// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace InfiniLore.Server.Data.Models;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UserContentAccessModel {
    [Key] public Guid Id { get; init; } = Guid.CreateVersion7(); // Not part of the BaseContent family
    public required Guid ContentId { get; init; }
    public required Guid UserId { get; init; }
    
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
