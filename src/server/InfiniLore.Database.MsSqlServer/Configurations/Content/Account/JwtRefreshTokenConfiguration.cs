// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.Account;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiniLore.Database.MsSqlServer.Configurations.Content.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class JwtRefreshTokenConfiguration : IEntityTypeConfiguration<JwtRefreshTokenModel> {

    public void Configure(EntityTypeBuilder<JwtRefreshTokenModel> builder) {
        builder.HasIndex(token => token.TokenHash)
            .IsUnique();
    }
}
