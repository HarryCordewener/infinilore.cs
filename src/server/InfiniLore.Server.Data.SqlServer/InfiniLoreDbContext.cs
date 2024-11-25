// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models;
using InfiniLore.Server.Data.Models.Content.Account;
using InfiniLore.Server.Data.Models.Content.UserData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace InfiniLore.Server.Data.SqlServer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniLoreDbContext : IdentityDbContext<InfiniLoreUser, IdentityRole, string> {
    public DbSet<LoreScopeModel> LoreScopes { get; init; }
    public DbSet<MultiverseModel> Multiverses { get; init; }
    public DbSet<UniverseModel> Universes { get; init; }
    public DbSet<JwtRefreshTokenModel> JwtRefreshTokens { get; init; }
    public DbSet<UserContentAccessModel> UserContentAccesses { get; init; }

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public InfiniLoreDbContext() {}
    public InfiniLoreDbContext(DbContextOptions<InfiniLoreDbContext> options) : base(options) {}

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        // Everything has been moved into ModelConfiguration files
        builder.ApplyConfigurationsFromAssembly(typeof(IAssemblyEntry).Assembly);

        // SEEDING DATA
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole("admin") { NormalizedName = "ADMIN" },
            new IdentityRole("user") { NormalizedName = "USER" }
        );

        var testUser = new InfiniLoreUser {
            Id = "d957c0f8-e90e-4068-a968-4f4b49fc165c",
            UserName = "testuser",
            NormalizedUserName = "TESTUSER",
            Email = "testuser@example.com",
            NormalizedEmail = "TESTUSER@EXAMPLE.COM",
            EmailConfirmed = true,
            SecurityStamp = "d957c0f8-e90e-4068-a968-4f4b49fc165b"
        };

        var hasher = new PasswordHasher<InfiniLoreUser>();
        testUser.PasswordHash = hasher.HashPassword(testUser, "Test@1234");

        builder.Entity<InfiniLoreUser>().HasData(testUser);
    }
}
