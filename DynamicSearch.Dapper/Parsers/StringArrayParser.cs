namespace DynamicSearch.Dapper.Parser;

public class StringArrayParser : IValueArrayParser<string>
{
    public string[] Parse(string value)
    {
        return value.TrimStart('[').TrimEnd(']').Split(',');
    }
}