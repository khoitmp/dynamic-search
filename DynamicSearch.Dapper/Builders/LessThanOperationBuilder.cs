namespace DynamicSearch.Dapper.Builder;

public class LessThanOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.LESS_THAN;

    public LessThanOperationBuilder(IValueParser<double> numbericParser,
                                    IValueParser<DateTime> dateTimeParser)
        : base(numbericParser: numbericParser, dateTimeParser: dateTimeParser)
    {
        SupportOperations.Clear();
        SupportOperations.Add(QueryType.NUMBER, BuildNumber);
        SupportOperations.Add(QueryType.DATE, BuildDate);
        SupportOperations.Add(QueryType.DATETIME, BuildDateTime);
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlNumber(string fieldName, double value)
    {
        string token = "@value";
        string query = $"{fieldName} < {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}::date < {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName} < {token}";
        return (query, new object[] { value }, new string[] { token });
    }
}