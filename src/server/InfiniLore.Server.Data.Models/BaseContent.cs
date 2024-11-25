// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfiniLore.Server.Data.Models;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class BaseContent {
    [Key] public Guid Id { get; init; } = Guid.CreateVersion7();
    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    public DateTime LastModifiedDate { get; private set; } = DateTime.UtcNow;
    public void UpdateLastModifiedDate() => LastModifiedDate = DateTime.UtcNow;

    #region SoftDelete
    [NotMapped] public bool IsSoftDeleted => SoftDeleteDate != null;
    public DateTime? SoftDeleteDate { get; private set; }
    public void SoftDelete() {
        SoftDeleteDate = DateTime.UtcNow;
        UpdateLastModifiedDate();
    }
    #endregion
}
