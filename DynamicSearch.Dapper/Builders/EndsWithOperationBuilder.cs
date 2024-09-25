namespace DynamicSearch.Dapper.Builder;

public class EndsWithOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.ENDS_WITH;

    public EndsWithOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser)
    {
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName} ilike concat('%', {token})";
        return (query, new object[] { value }, new string[] { token });
    }
}