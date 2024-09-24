namespace DynamicSearch.EfCore.Service;

internal class NotContainsOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.NOT_CONTAINS;

    public NotContainsOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser)
    {
        SupportedQueryTypes.Clear();
        SupportedQueryTypes.Add(QueryType.TEXT, BuildText);
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName}.Contains({token}) == false";
        return (query, new string[] { token }, new object[] { value });
    }
}