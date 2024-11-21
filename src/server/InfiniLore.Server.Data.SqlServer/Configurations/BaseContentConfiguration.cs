// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Server.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiniLore.Server.Data.SqlServer.Configurations;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class BaseContentConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseContent {
    public virtual void Configure(EntityTypeBuilder<T> builder) {
        HasSoftDeleteAsQueryFilter(builder);
        HasUniqueIdAsKey(builder);
    }

    protected static void HasSoftDeleteAsQueryFilter(EntityTypeBuilder<T> builder) {
        builder.HasQueryFilter(model => model.SoftDeleteDate == null);
    }

    protected static void HasUniqueIdAsKey(EntityTypeBuilder<T> builder) {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
    }
}
