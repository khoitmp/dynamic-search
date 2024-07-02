namespace Core.Application.Repository.Interface;

public interface ICoreUnitOfWork : IUnitOfWork
{
    IDeviceRepository DeviceRepository { get; }
}