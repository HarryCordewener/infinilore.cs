// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Account;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiniLore.Server.Data.Configurations.Account;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class JwtRefreshTokenConfiguration : IEntityTypeConfiguration<JwtRefreshTokenModel> {

    public void Configure(EntityTypeBuilder<JwtRefreshTokenModel> builder) {
        builder.HasIndex(token => token.TokenHash)
            .IsUnique();
    }
}
