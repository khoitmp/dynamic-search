namespace DynamicSearch.EfCore.Service;

internal class StartsWithOperationBuilder : ContainsOperationBuilder
{
    protected override string Operation => Operations.STARTS_WITH;

    public StartsWithOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser)
    {
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string sql = $"{fieldName}.StartsWith({token})";
        return (sql, new string[] { token }, new object[] { value });
    }
}