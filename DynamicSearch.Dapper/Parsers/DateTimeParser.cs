namespace DynamicSearch.Dapper.Parser;

public class DateTimeParser : IValueParser<DateTime>
{
    public DateTime Parse(string value)
    {
        return DateTime.ParseExact(value, Defaults.DefaultFullDateTimeFormat, CultureInfo.InvariantCulture);
    }
}