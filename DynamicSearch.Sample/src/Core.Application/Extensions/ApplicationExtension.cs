namespace Core.ApplicationExtension.Extension;

public static class ApplicationExtension
{
    public const string SERVICE_NAME = "device-service";

    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        // Lib services
        serviceCollection.AddDynamicSearch();
        serviceCollection.AddMediatR(typeof(ApplicationExtension).GetTypeInfo().Assembly);

        // Internal services
        serviceCollection.AddScoped<IDeviceService, DeviceService>();
    }
}