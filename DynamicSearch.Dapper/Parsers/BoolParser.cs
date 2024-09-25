namespace DynamicSearch.Dapper.Parser;

public class BoolParser : IValueParser<bool>
{
    public bool Parse(string value)
    {
        return bool.Parse(value);
    }
}