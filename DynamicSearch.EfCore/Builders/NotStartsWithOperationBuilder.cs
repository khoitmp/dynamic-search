namespace DynamicSearch.EfCore.Service;

internal class NotStartsWithOperationBuilder : ContainsOperationBuilder
{
    protected override string Operation => Operations.NOT_STARTS_WITH;

    public NotStartsWithOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser)
    {
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName}.StartsWith({token}) == false";
        return (query, new string[] { token }, new object[] { value });
    }
}