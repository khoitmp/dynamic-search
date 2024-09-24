namespace DynamicSearch.EfCore.Service;

internal class NotEndsWithOperationBuilder : ContainsOperationBuilder
{
    protected override string Operation => Operations.NOT_ENDS_WITH;

    public NotEndsWithOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser)
    {
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string sql = $"{fieldName}.EndsWith({token}) == false";
        return (sql, new string[] { token }, new object[] { value });
    }
}