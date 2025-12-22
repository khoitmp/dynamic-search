namespace Core.Api;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddPersistenceServices();
        services.AddControllers()
            .AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.DefaultIgnoreCondition = JsonSettings.JsonSerializerOptions.DefaultIgnoreCondition;
                option.JsonSerializerOptions.PropertyNamingPolicy = JsonSettings.JsonSerializerOptions.PropertyNamingPolicy;
                option.JsonSerializerOptions.ReferenceHandler = JsonSettings.JsonSerializerOptions.ReferenceHandler;
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}