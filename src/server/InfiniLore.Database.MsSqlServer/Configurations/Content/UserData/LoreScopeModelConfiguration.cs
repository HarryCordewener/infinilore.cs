// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Database.Models.Content.UserData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiniLore.Database.MsSqlServer.Configurations.Content.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LorescopeModelConfiguration : UserContentConfiguration<LorescopeModel> {

    public override void Configure(EntityTypeBuilder<LorescopeModel> builder) {
        base.Configure(builder);

        builder.HasIndex(model => new { model.Name, model.OwnerId })
            .IsUnique();

        builder.HasMany(model => model.Multiverses)
            .WithOne(multiverse => multiverse.Lorescope)
            .HasForeignKey(x => x.LorescopeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
