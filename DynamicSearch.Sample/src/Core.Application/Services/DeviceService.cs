using DynamicSearch.Dapper.Interface;
using DynamicSearch.Dapper.Model;

namespace Core.Application.Service;

public class DeviceService : BaseSearchService<Device, string, SearchDevicesCommand, DeviceDto>, IDeviceService
{
    private readonly IQueryService _queryService;
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IServiceProvider serviceProvider, IQueryService queryService, IDeviceRepository deviceRepository)
        : base(serviceProvider, DeviceDto.Create)
    {
        _queryService = queryService;
        _deviceRepository = deviceRepository;
    }

    public Task<IEnumerable<object>> QueryDevices(QueryCriteria criteria)
    {
        var tableName = "devices";
        var query = $"select * from {tableName.ToStringQuote()}";
        (query, var value) = _queryService.CompileQuery(query, criteria);
        return _deviceRepository.GetDevicesAsync(query, value);
    }

    protected override Type GetDbType()
    {
        return typeof(IDeviceRepository);
    }
}