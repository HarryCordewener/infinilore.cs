// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace InfiniLore.Server.Data.Models.Base;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class UserContent<T> : BaseContent<T> where T : BaseContent<T> {
    public required InfiniLoreUser Owner { get; set; }
    [MaxLength(48)] public string OwnerId { get; set; } = null!;
    
    public bool IsPublic { get; set; }
    public ICollection<UserContentAccess<T>> UserAccess { get; set; } = [];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public abstract void Update(T model);
}