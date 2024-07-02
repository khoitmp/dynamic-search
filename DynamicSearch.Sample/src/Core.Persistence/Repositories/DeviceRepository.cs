namespace Core.Persistence.Repository
{
    public class DeviceRepository : GenericRepository<Device, string>, IDeviceRepository
    {
        private CoreDbContext _context;

        public DeviceRepository(CoreDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public override IQueryable<Device> AsQueryable() => _context.Devices.Include(x => x.Type);

        protected override void Update(Device requestObject, Device targetObject)
        {
            targetObject.Name = requestObject.Name;
            targetObject.TypeId = requestObject.TypeId;
            targetObject.UpdatedUtc = DateTime.UtcNow;
        }
    }
}