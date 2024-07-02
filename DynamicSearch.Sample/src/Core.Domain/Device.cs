namespace Core.Domain.Entity;

public class Device : IEntity<string>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string TypeId { get; set; }
    public DeviceType Type { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
    public bool Deleted { set; get; }
}