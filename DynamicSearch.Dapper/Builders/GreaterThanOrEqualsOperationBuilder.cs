namespace DynamicSearch.Dapper.Builder;

public class GreaterThanOrEqualsOperationBuilder : GreaterThanOperationBuilder
{
    protected override string Operation => Operations.GREATER_THAN_OR_EQUALS;

    public GreaterThanOrEqualsOperationBuilder(IValueParser<double> numbericParser,
                                            IValueParser<DateTime> dateTimeParser)
        : base(numbericParser: numbericParser, dateTimeParser: dateTimeParser)
    {
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlNumber(string fieldName, double value)
    {
        string token = "@value";
        string query = $"{fieldName} >= {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}::date >= {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName} >= {token}";
        return (query, new object[] { value }, new string[] { token });
    }
}