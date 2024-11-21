// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models.Content.UserData;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiniLore.Server.Data.SqlServer.Configurations.Content.UserData;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UniverseModelConfiguration : UserContentConfiguration<UniverseModel> {

    public override void Configure(EntityTypeBuilder<UniverseModel> builder) {
        base.Configure(builder);

        builder.HasQueryFilter(model => model.SoftDeleteDate == null);

        builder.HasIndex(model => new { model.Name, model.MultiverseId })
            .IsUnique();
    }
}
