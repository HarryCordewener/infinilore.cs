// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;

namespace InfiniLore.Server.Data.Models.Base;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UserContentAccess<T> : BaseContent<T> where T : BaseContent<T> {
    public required InfiniLoreUser User { get; set; }
    public required AccessLevel AccessLevel { get; set; }
}

public enum AccessLevel {
    Read,
    Write,
    Manage,
    Owner
}
