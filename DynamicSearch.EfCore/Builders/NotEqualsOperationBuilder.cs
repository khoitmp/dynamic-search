namespace DynamicSearch.EfCore.Service;

internal class NotEqualsOperationBuilder : EqualsOperationBuilder
{
    protected override string Operation => Operations.NOT_EQUALS;

    public NotEqualsOperationBuilder(IValueParser<string> stringParser,
                                        IValueParser<double> numbericParser,
                                        IValueParser<bool> boolParser,
                                        IValueParser<Guid> guidParser,
                                        IValueParser<DateTime> dateParser)
        : base(stringParser, numbericParser, boolParser, guidParser, dateParser)
    {
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName} != {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNumber(string fieldName, double value)
    {
        string token = "@value";
        string query = $"{fieldName} != {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlBoolean(string fieldName, bool value)
    {
        string token = "@value";
        string query = $"{fieldName} != {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlGuid(string fieldName, Guid value)
    {
        string token = "@value";
        string query = $"{fieldName} != {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableGuid(string fieldName, Guid value)
    {
        string token = "@value";
        string query = $"{fieldName}.Value != {token}.Value";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Date == {token}.Date";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Value.Date != {token}.Value.Date";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName} != {token}";
        return (query, new string[] { token }, new object[] { value });
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}.Value != {token}.Value";
        return (query, new string[] { token }, new object[] { value });
    }
}