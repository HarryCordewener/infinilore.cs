// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.Account;

namespace InfiniLore.Database.Models;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// In a perfect world this should have been in InfiniLore.Server.Contracts but because this is a shared library, we can't
// reference the server project.
public interface IHasOwner {
    public InfiniLoreUser Owner { get; set; }
    public Guid OwnerId { get; set; }
}
