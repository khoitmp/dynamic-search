namespace DynamicSearch.Dapper.Parser;

public class StringParser : IValueParser<string>
{
    public string Parse(string value)
    {
        return value;
    }
}