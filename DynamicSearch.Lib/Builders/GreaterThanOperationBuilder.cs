namespace DynamicSearch.Lib.Service;

internal class GreaterThanOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.GREATER_THAN;

    public GreaterThanOperationBuilder(IValueParser<double> numbericParser,
                                        IValueParser<DateTime> dateParser)
        : base(numbericParser: numbericParser, dateParser: dateParser)
    {
        SupportedQueryTypes.Clear();
        SupportedQueryTypes.Add(QueryType.NUMBER, BuildNumber);
        SupportedQueryTypes.Add(QueryType.DATE, BuildDate);
        SupportedQueryTypes.Add(QueryType.NULLABLE_DATE, BuildNullableDate);
        SupportedQueryTypes.Add(QueryType.DATETIME, BuildDateTime);
        SupportedQueryTypes.Add(QueryType.NULLABLE_DATETIME, BuildNullableDateTime);
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNumber(string fieldName, double value)
    {
        string token = "@value";
        string query = $"{fieldName} > {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Date > {token}.Date";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Value.Date > {token}.Value.Date";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName} > {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Value > {token}.Value";
        return (query, new string[] { token }, new object[] { value });
    }
}