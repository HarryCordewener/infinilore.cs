// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.UserData;
using Microsoft.AspNetCore.Identity;

namespace InfiniLore.Database.Models.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniLoreUser : IdentityUser<Guid> {
    public ICollection<LorescopeModel> Lorescopes { get; init; } = [];
    public ICollection<MultiverseModel> Multiverses { get; init; } = [];
    public ICollection<UniverseModel> Universes { get; init; } = [];
    public ICollection<JwtRefreshTokenModel> JwtRefreshTokens { get; init; } = [];

    public ICollection<UserContentAccessModel> ContentAccesses { get; init; } = [];

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    // public InfiniLoreUser() {}
    // public InfiniLoreUser(string username) : base(username) {} // Solves an issue with FastEndpoints
}
