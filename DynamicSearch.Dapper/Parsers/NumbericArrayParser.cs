namespace DynamicSearch.Dapper.Parser;

public class NumbericArrayParser : IValueArrayParser<double>
{
    public double[] Parse(string value)
    {
        var filterArray = value.TrimStart('[').TrimEnd(']').Split(',');
        return filterArray.Select(x => double.Parse(x.ToString())).ToArray();
    }
}