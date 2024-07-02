namespace Core.Persistence.Configuration;

public class DeviceTypeConfiguration : IEntityTypeConfiguration<DeviceType>
{
    public void Configure(EntityTypeBuilder<DeviceType> builder)
    {
        builder.ToTable("device_types");
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.UpdatedUtc).HasColumnName("updated_utc");
        builder.Property(x => x.CreatedUtc).HasColumnName("created_utc");
        builder.Property(x => x.Deleted).HasColumnName("deleted");
        builder.HasQueryFilter(x => !x.Deleted);
    }
}