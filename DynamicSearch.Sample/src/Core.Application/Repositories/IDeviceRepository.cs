namespace Core.Application.Repository.Interface;

public interface IDeviceRepository : IRepository<Device, string>
{
    Task<IEnumerable<object>> GetDevicesAsync(string query, ExpandoObject value = null);
}