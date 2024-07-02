namespace Core.Domain.Entity;

public class DeviceType : IEntity<string>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
    public bool Deleted { set; get; }
}