namespace DynamicSearch.EfCore.Service;

internal class GreaterThanOrEqualsOperationBuilder : GreaterThanOperationBuilder
{
    protected override string Operation => Operations.GREATER_THAN_OR_EQUALS;

    public GreaterThanOrEqualsOperationBuilder(IValueParser<double> numbericParser,
                                                IValueParser<DateTime> dateParser)
        : base(numbericParser: numbericParser, dateParser: dateParser)
    {
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNumber(string fieldName, double value)
    {
        string token = "@value";
        string query = $"{fieldName} >= {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Date >= {token}.Date";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Value.Date >= {token}.Value.Date";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName} >= {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Value >= {token}.Value";
        return (query, new string[] { token }, new object[] { value });
    }
}