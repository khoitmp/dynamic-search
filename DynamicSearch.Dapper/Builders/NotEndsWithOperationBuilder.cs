namespace DynamicSearch.Dapper.Builder;

public class NotEndsWithOperationBuilder : ContainsOperationBuilder
{
    protected override string Operation => Operations.NOT_ENDS_WITH;

    public NotEndsWithOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser)
    {
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName} not ilike concat('%', {token})";
        return (query, new object[] { value }, new string[] { token });
    }
}