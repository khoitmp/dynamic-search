namespace Core.Application.Handler;

public class SearchDevicesRequestHandler : IRequestHandler<SearchDevicesCommand, BaseSearchResponse<DeviceDto>>
{
    private readonly IDeviceService _service;

    public SearchDevicesRequestHandler(IDeviceService service)
    {
        _service = service;
    }

    public Task<BaseSearchResponse<DeviceDto>> Handle(SearchDevicesCommand request, CancellationToken cancellationToken)
    {
        return _service.SearchAsync(request);
    }
}