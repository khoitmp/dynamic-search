namespace Core.Persistence.Context;

public class CoreDbContext : DbContext
{
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<Device> Devices { get; set; }

    public CoreDbContext(DbContextOptions<CoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeviceTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
    }
}