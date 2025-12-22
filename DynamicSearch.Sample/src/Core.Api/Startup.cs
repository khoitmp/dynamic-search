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
                option.JsonSerializerOptions.DefaultIgnoreCondition = Defaults.JsonSerializerOptions.DefaultIgnoreCondition;
                option.JsonSerializerOptions.PropertyNamingPolicy = Defaults.JsonSerializerOptions.PropertyNamingPolicy;
                option.JsonSerializerOptions.ReferenceHandler = Defaults.JsonSerializerOptions.ReferenceHandler;
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