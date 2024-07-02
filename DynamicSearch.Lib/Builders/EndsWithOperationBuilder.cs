namespace DynamicSearch.Lib.Service;

internal class EndsWithOperationBuilder : BaseBuilder
{
    protected override string Operation => Operations.ENDS_WITH;

    public EndsWithOperationBuilder(IValueParser<string> stringParser)
        : base(stringParser: stringParser)
    {
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string value)
    {
        string token = "@value";
        string query = $"{fieldName}.EndsWith({token})";
        return (query, new string[] { token }, new object[] { value });
    }
}