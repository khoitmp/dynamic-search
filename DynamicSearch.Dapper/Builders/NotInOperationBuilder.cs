namespace DynamicSearch.Dapper.Builder;

public class NotInOperationBuilder : InOperationBuilder
{
    protected override string Operation => Operations.NOT_IN;

    public NotInOperationBuilder(IValueArrayParser<string> stringParser,
                                IValueArrayParser<double> numbericParser,
                                IValueArrayParser<Guid> guidParser,
                                IValueArrayParser<DateTime> dateTimeParser)
        : base(stringParser, numbericParser, guidParser, dateTimeParser)
    {
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlText(string fieldName, string[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName.Replace(".ToLower()", "")} <> {token}");
            listToken.Add(token);
        }
        return ($" ( {string.Join(" and ", listQuery.ToArray())} )", values, listToken.ToArray());
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlNumber(string fieldName, double[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName} <> {token}");
            listToken.Add(token);
        }
        return ($" ( {string.Join(" and ", listQuery.ToArray())} )", values.Select(x => x as object).ToArray(), listToken.ToArray());
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlGuid(string fieldName, Guid[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName} <> {token}");
            listToken.Add(token);
        }
        return ($" ( {string.Join(" and ", listQuery.ToArray())} )", values.Select(x => x as object).ToArray(), listToken.ToArray());
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlDate(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName}::date <> {token}");
            listToken.Add(token);
        }
        return ($"( {string.Join(" and ", listQuery.ToArray())} )", values.Select(x => x as object).ToArray(), listToken.ToArray());
    }

    protected override (string Query, object[] Values, string[] Tokens) BuildOperationSqlDateTime(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName} <> {token}");
            listToken.Add(token);
        }
        return ($"( {string.Join(" and ", listQuery.ToArray())} )", values.Select(x => x as object).ToArray(), listToken.ToArray());
    }
}