namespace Core.Persistence.Configuration;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("devices");
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.TypeId).HasColumnName("type_id");
        builder.Property(x => x.UpdatedUtc).HasColumnName("updated_utc");
        builder.Property(x => x.CreatedUtc).HasColumnName("created_utc");
        builder.Property(x => x.Deleted).HasColumnName("deleted");
        builder.HasQueryFilter(x => !x.Deleted);
    }
}