// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace InfiniLore.Server.Data.Models.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class JwtRefreshTokenModel : BaseContent, IHasOwner {

    [MaxLength(64)] public required string TokenHash { get; init; }
    public required DateTime ExpiresAt { get; init; }

    public string[] Roles { get; init; } = [];
    public string[] Permissions { get; init; } = [];
    public int? ExpiresInDays { get; init; }
    public required InfiniLoreUser Owner { get; set; }
    [MaxLength(64)] public string OwnerId { get; set; } = null!;
}
