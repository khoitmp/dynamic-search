namespace DynamicSearch.Dapper.Parser;

public class NumbericParser : IValueParser<double>
{
    public double Parse(string value)
    {
        return double.Parse(value);
    }
}