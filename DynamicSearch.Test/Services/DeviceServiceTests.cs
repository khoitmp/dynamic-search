using Core.Persistence.Context;
using Core.Persistence.Repository;
using Core.Domain.Entity;
using Core.Application.Repository.Interface;
using Core.Application.Service;
using DynamicSearch.EfCore.Extension;
using Microsoft.EntityFrameworkCore;

namespace DynamicSearch.Test.Service;

public class DeviceServiceTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;
    private readonly string _databaseName;

    public DeviceServiceTests()
    {
        _databaseName = "database_" + Guid.NewGuid();

        var services = new ServiceCollection();

        // Add configuration
        var configuration = new ConfigurationBuilder().Build();
        services.AddSingleton<IConfiguration>(configuration);

        // Configure in-memory database - use scoped lifetime but with shared instance
        services.AddDbContext<CoreDbContext>(options =>
            options.UseInMemoryDatabase(_databaseName)
                   .EnableSensitiveDataLogging());

        // Add EF Core dynamic search services
        services.AddEfCoreDynamicSearch();

        // Add repositories
        services.AddScoped<IDeviceRepository, DeviceRepository>();

        // Add application services
        services.AddScoped<IDeviceService, DeviceService>();

        _serviceProvider = services.BuildServiceProvider();

        // Seed test data
        SeedData();
    }

    [Fact]
    public async Task SearchDevices()
    {
        var command = new SearchDevicesCommand();
        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(5, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_Equals()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "name" },
                { "queryType", "text" },
                { "operation", "eq" },
                { "queryValue", "Device 1" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(1, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_NotEquals()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "name" },
                { "queryType", "text" },
                { "operation", "neq" },
                { "queryValue", "Device 1" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(4, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_In()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "name" },
                { "queryType", "text" },
                { "operation", "in" },
                { "queryValue", "[Device 1,Device 2]" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(2, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_NotIn()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "name" },
                { "queryType", "text" },
                { "operation", "nin" },
                { "queryValue", "[Device 1,Device 2]" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(3, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_LessThan()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "createdUtc" },
                { "queryType", "datetime" },
                { "operation", "lt" },
                { "queryValue", "2024-01-03T00:00:00:0000" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(2, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_LessThanOrEqualsTo()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "createdUtc" },
                { "queryType", "datetime" },
                { "operation", "lte" },
                { "queryValue", "2024-01-03T00:00:00:0000" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(3, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_GreaterThan()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "createdUtc" },
                { "queryType", "datetime" },
                { "operation", "gt" },
                { "queryValue", "2024-01-03T00:00:00:0000" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(2, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_GreaterThanOrEqualsTo()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "createdUtc" },
                { "queryType", "datetime" },
                { "operation", "gte" },
                { "queryValue", "2024-01-03T00:00:00:0000" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(3, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_Contains_Ref()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "type.name" },
                { "queryType", "text" },
                { "operation", "contains" },
                { "queryValue", "tat" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(1, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_NotContains_Ref()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "type.name" },
                { "queryType", "text" },
                { "operation", "ncontains" },
                { "queryValue", "tat" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(4, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_Between_Ref()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "type.createdUtc" },
                { "queryType", "datetime" },
                { "operation", "between" },
                { "queryValue", "[2024-01-01T00:00:00:0000,2024-01-02T00:00:00:0000]" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(2, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_NotBetween_Ref()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "type.createdUtc" },
                { "queryType", "datetime" },
                { "operation", "nbetween" },
                { "queryValue", "[2024-01-01T00:00:00:0000,2024-01-02T00:00:00:0000]" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(3, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_StartsWith_Ref()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "type.name" },
                { "queryType", "text" },
                { "operation", "sw" },
                { "queryValue", "S" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(1, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_NotStartsWith_Ref()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "queryKey", "type.name" },
                { "queryType", "text" },
                { "operation", "nsw" },
                { "queryValue", "S" }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(4, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_EndsWith_Ref_Multiple()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "and", new JsonArray
                    {
                        new JsonObject
                        {
                            { "queryKey", "name" },
                            { "queryType", "text" },
                            { "operation", "ew" },
                            { "queryValue", "1" }
                        },
                        new JsonObject
                        {
                            { "queryKey", "type.name" },
                            { "queryType", "text" },
                            { "operation", "ew" },
                            { "queryValue", "c" }
                        }
                    }
                }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(1, response.TotalCount);
    }

    [Fact]
    public async Task SearchDevices_NotEndsWith_Ref_Multiple()
    {
        var command = new SearchDevicesCommand
        {
            Filter = new JsonObject
            {
                { "or", new JsonArray
                    {
                        new JsonObject
                        {
                            { "queryKey", "type.name" },
                            { "queryType", "text" },
                            { "operation", "new" },
                            { "queryValue", "c" }
                        }
                    }
                }
            }
        };

        var response = await ExecuteSearchAsync(command);

        Xunit.Assert.Equal(3, response.TotalCount);
    }

    public void Dispose()
    {
        _serviceProvider?.Dispose();
    }

    private async Task<BaseSearchResponse<DeviceDto>> ExecuteSearchAsync(SearchDevicesCommand command)
    {
        using var scope = _serviceProvider.CreateScope();
        var deviceService = scope.ServiceProvider.GetRequiredService<IDeviceService>();
        return await deviceService.SearchAsync(command);
    }

    private void SeedData()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CoreDbContext>();

        var deviceTypes = new[]
        {
            new DeviceType { Id = "STATIC", Name = "Static", CreatedUtc = new DateTime(2024, 1, 1), UpdatedUtc = DateTime.UtcNow, Deleted = false },
            new DeviceType { Id = "DYNAMIC", Name = "Dynamic", CreatedUtc = new DateTime(2024, 1, 2), UpdatedUtc = DateTime.UtcNow, Deleted = false },
            new DeviceType { Id = "RUNTIME", Name = "Runtime", CreatedUtc = new DateTime(2024, 1, 3), UpdatedUtc = DateTime.UtcNow, Deleted = false },
            new DeviceType { Id = "ALIAS", Name = "Alias", CreatedUtc = new DateTime(2024, 1, 4), UpdatedUtc = DateTime.UtcNow, Deleted = false },
            new DeviceType { Id = "COMMAND", Name = "Command", CreatedUtc = new DateTime(2024, 1, 5), UpdatedUtc = DateTime.UtcNow, Deleted = false }
        };

        var devices = new[]
        {
            new Device { Id = "device1", Name = "Device 1", TypeId = "STATIC", CreatedUtc = new DateTime(2024, 1, 1), UpdatedUtc = DateTime.UtcNow, Deleted = false },
            new Device { Id = "device2", Name = "Device 2", TypeId = "DYNAMIC", CreatedUtc = new DateTime(2024, 1, 2), UpdatedUtc = DateTime.UtcNow, Deleted = false },
            new Device { Id = "device3", Name = "Device 3", TypeId = "RUNTIME", CreatedUtc = new DateTime(2024, 1, 3), UpdatedUtc = DateTime.UtcNow, Deleted = false },
            new Device { Id = "device4", Name = "Device 4", TypeId = "ALIAS", CreatedUtc = new DateTime(2024, 1, 4), UpdatedUtc = DateTime.UtcNow, Deleted = false },
            new Device { Id = "device5", Name = "Device 5", TypeId = "COMMAND", CreatedUtc = new DateTime(2024, 1, 5), UpdatedUtc = DateTime.UtcNow, Deleted = false }
        };

        dbContext.DeviceTypes.AddRange(deviceTypes);
        dbContext.Devices.AddRange(devices);
        dbContext.SaveChanges();
    }
}