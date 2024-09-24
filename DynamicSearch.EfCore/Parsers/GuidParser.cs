namespace DynamicSearch.EfCore.Service;

internal class GuidParser : IValueParser<Guid>
{
    public Guid Parse(string value)
    {
        return Guid.Parse(value);
    }
}