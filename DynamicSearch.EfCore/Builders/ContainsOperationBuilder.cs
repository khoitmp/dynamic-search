namespace DynamicSearch.EfCore.Service;

internal class ContainsOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.CONTAINS;

    public ContainsOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser: stringParser)
    {
        SupportedQueryTypes.Clear();
        SupportedQueryTypes.Add(QueryType.TEXT, BuildText);
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName}.Contains({token})";
        return (query, new string[] { token }, new object[] { value });
    }
}