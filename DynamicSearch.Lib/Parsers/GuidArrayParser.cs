namespace DynamicSearch.Lib.Service;

internal class GuidArrayParser : IValueArrayParser<Guid>
{
    public Guid[] Parse(string value)
    {
        var array = value.TrimStart('[').TrimEnd(']').Split(',');
        return array.Select(x => Guid.Parse(x)).ToArray();
    }
}