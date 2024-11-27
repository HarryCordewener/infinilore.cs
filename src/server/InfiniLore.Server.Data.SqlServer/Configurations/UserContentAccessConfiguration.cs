// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiniLore.Server.Data.SqlServer.Configurations;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UserContentAccessConfiguration : IEntityTypeConfiguration<UserContentAccessModel> {
    public void Configure(EntityTypeBuilder<UserContentAccessModel> builder) {
        
        // Each user can only have one unique access level per content
        // example : A user can have both read and write access rights, but not twice read access rights
        builder.HasIndex(content => new { content.ContentId, content.UserId })
            .IsUnique();
    } 
}
