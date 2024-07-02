namespace Core.Application.Command;

public class SearchDevicesCommand : BaseSearchCriteria, IRequest<BaseSearchResponse<DeviceDto>>
{
    public bool ClientOverride { get; set; } = false;

    public SearchDevicesCommand()
    {
        PageSize = 20;
        PageIndex = 0;
    }
}