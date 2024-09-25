namespace DynamicSearch.Dapper.Builder;

public class StartsWithOperationBuilder : ContainsOperationBuilder
{
    protected override string Operation => Operations.STARTS_WITH;

    public StartsWithOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser)
    {
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName} ilike concat({token}, '%')";
        return (query, new object[] { value }, new string[] { token });
    }
}