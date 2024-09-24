namespace DynamicSearch.EfCore.Service;

internal class StringParser : IValueParser<string>
{
    public string Parse(string value)
    {
        return value;
    }
}