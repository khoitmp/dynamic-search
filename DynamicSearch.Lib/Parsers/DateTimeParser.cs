namespace DynamicSearch.Lib.Service;

internal class DateTimeParser : IValueParser<DateTime>
{
    public DateTime Parse(string value)
    {
        var dateTime = DateTime.ParseExact(value, Defaults.DefaultFullDateTimeFormat, CultureInfo.InvariantCulture);
        var utcDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        return utcDateTime;
    }
}