namespace DynamicSearch.Dapper.Builder;

public class NotContainsOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.NOT_CONTAINS;

    public NotContainsOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser)
    {
        SupportOperations.Clear();
        SupportOperations.Add(QueryType.TEXT, BuildText);
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName} not ilike concat('%', {token}, '%')";
        return (query, new object[] { value }, new string[] { token });
    }
}