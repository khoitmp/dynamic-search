namespace DynamicSearch.Lib.Service;

internal class NumbericParser : IValueParser<double>
{
    public double Parse(string value)
    {
        return double.Parse(value);
    }
}