namespace DynamicSearch.Lib.Service;

internal class NumbericArrayParser : IValueArrayParser<double>
{
    public double[] Parse(string value)
    {
        var array = value.TrimStart('[').TrimEnd(']').Split(',');
        return array.Select(x => double.Parse(x.ToString())).ToArray();
    }
}