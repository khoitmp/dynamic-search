namespace DynamicSearch.Dapper.Builder;

public class NotEqualsOperationBuilder : EqualsOperationBuilder
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

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName} <> {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlNumber(string fieldName, double value)
    {
        string token = "@value";
        string query = $"{fieldName} <> {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlBoolean(string fieldName, bool value)
    {
        string token = "@value";
        string query = $"{fieldName} <> {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlGuid(string fieldName, Guid value)
    {
        string token = "@value";
        string query = $"{fieldName} <> {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlDate(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName}::date <> {token}";
        return (query, new object[] { value }, new string[] { token });
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlDateTime(string fieldName, DateTime value)
    {
        string token = "@value";
        string query = $"{fieldName} <> {token}";
        return (query, new object[] { value }, new string[] { token });
    }
}