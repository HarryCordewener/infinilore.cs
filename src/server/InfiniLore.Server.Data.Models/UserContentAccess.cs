// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.Account;

namespace InfiniLore.Server.Data.Models;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UserContentAccess : BaseContent {
    public required InfiniLoreUser User { get; set; }
    public required AccessLevel AccessLevel { get; set; }
}

public enum AccessLevel {
    Read,
    Write,
    Manage,
    Owner
}
