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
            .AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.DateFormatString = Defaults.JsonSerializerSetting.DateFormatString;
                option.SerializerSettings.ReferenceLoopHandling = Defaults.JsonSerializerSetting.ReferenceLoopHandling;
                option.SerializerSettings.DateParseHandling = Defaults.JsonSerializerSetting.DateParseHandling;
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