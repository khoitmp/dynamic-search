namespace Core.Application.Model;

public class DeviceDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
    public DeviceTypeDto Type { get; set; }

    private static Func<Device, DeviceDto> Converter = Projection.Compile();

    public static Expression<Func<Device, DeviceDto>> Projection
    {
        get
        {
            return entity => new DeviceDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedUtc = entity.CreatedUtc,
                UpdatedUtc = entity.UpdatedUtc,
                Type = DeviceTypeDto.Create(entity.Type)
            };
        }
    }

    public static DeviceDto Create(Device entity)
    {
        if (entity == null)
            return null;
        return Converter(entity);
    }
}