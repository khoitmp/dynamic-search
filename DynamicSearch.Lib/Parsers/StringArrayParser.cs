namespace DynamicSearch.Lib.Service;

internal class StringArrayParser : IValueArrayParser<string>
{
    public string[] Parse(string value)
    {
        return value.TrimStart('[').TrimEnd(']').Split(',');
    }
}