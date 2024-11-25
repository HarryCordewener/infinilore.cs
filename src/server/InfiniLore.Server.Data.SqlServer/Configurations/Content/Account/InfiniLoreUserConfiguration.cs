// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.Account;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiniLore.Server.Data.SqlServer.Configurations.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class InfiniLoreUserConfiguration : IEntityTypeConfiguration<InfiniLoreUser> {

    public void Configure(EntityTypeBuilder<InfiniLoreUser> builder) {
        builder.HasMany(user => user.LoreScopes)
            .WithOne(scope => scope.Owner)
            .HasForeignKey(x => x.OwnerId);

        builder.HasMany(user => user.Multiverses)
            .WithOne(multiverse => multiverse.Owner)
            .HasForeignKey(x => x.OwnerId);

        builder.HasMany(user => user.Universes)
            .WithOne(universe => universe.Owner)
            .HasForeignKey(x => x.OwnerId);

        builder.HasMany(user => user.JwtRefreshTokens)
            .WithOne(token => token.Owner)
            .HasForeignKey(token => token.OwnerId);

        builder.HasMany(user => user.ContentAccesses)
            .WithOne(access => access.User)
            .HasForeignKey(access => access.UserId);
    }
}
