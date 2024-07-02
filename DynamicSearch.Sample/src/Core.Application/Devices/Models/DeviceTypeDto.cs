namespace Core.Application.Model;

public class DeviceTypeDto
{
    public string Id { get; set; }
    public string Name { get; set; }

    private static Func<DeviceType, DeviceTypeDto> Converter = Projection.Compile();

    public static Expression<Func<DeviceType, DeviceTypeDto>> Projection
    {
        get
        {
            return entity => new DeviceTypeDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }

    public static DeviceTypeDto Create(DeviceType entity)
    {
        if (entity == null)
            return null;
        return Converter(entity);
    }
}