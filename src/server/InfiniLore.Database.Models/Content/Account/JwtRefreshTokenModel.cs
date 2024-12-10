// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace InfiniLore.Database.Models.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class JwtRefreshTokenModel : BaseContent, IHasOwner {

    [MaxLength(64)] public required string TokenHash { get; init; }
    public required DateTime ExpiresAt { get; init; }

    public string[] Roles { get; init; } = [];
    public string[] Permissions { get; init; } = [];
    public int? ExpiresInDays { get; init; }
    public InfiniLoreUser Owner { get; set; } = default!;
    public Guid OwnerId { get; set; }
}
