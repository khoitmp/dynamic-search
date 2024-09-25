namespace DynamicSearch.Dapper.Parser;

public class GuidParser : IValueParser<Guid>
{
    public Guid Parse(string value)
    {
        return Guid.Parse(value);
    }
}