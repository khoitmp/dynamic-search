namespace Core.Persistence.Extension;

public static class PersistenceExtension
{
    public static void AddPersistenceServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<CoreDbContext>((service, option) =>
        {
            var configuration = service.GetService<IConfiguration>();
            var connectionString = configuration["ConnectionStrings:Default"];
            option.UseNpgsql(connectionString);
        });

        serviceCollection.AddScoped<ICoreUnitOfWork, CoreUnitOfWork>();
        serviceCollection.AddScoped<IDeviceRepository, DeviceRepository>();
    }
}