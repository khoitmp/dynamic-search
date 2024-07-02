namespace Core.Persistence.Repository;

public class CoreUnitOfWork : BaseUnitOfWork, ICoreUnitOfWork
{
    public IDeviceRepository DeviceRepository { get; }

    public CoreUnitOfWork(
        CoreDbContext context,
        IDeviceRepository deviceRepository)
        : base(context)
    {
        DeviceRepository = deviceRepository;
    }
}