// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.UserData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiniLore.Server.Data.SqlServer.Configurations.Content.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LoreScopeModelConfiguration : UserContentConfiguration<LoreScopeModel> {

    public override void Configure(EntityTypeBuilder<LoreScopeModel> builder) {
        base.Configure(builder);

        builder.HasIndex(model => new { model.Name, model.OwnerId })
            .IsUnique();

        builder.HasMany(model => model.Multiverses)
            .WithOne(multiverse => multiverse.LoreScope)
            .HasForeignKey(x => x.LoreScopeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
