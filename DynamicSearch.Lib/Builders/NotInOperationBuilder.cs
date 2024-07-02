namespace DynamicSearch.Lib.Service;

internal class NotInOperationBuilder : InOperationBuilder
{
    protected override string Operation => Operations.NOT_IN;

    public NotInOperationBuilder(IValueArrayParser<string> stringParser,
                                    IValueArrayParser<double> numbericParser,
                                    IValueArrayParser<Guid> guidParser,
                                    IValueArrayParser<DateTime> dateTimeParser)
        : base(stringParser, numbericParser, guidParser, dateTimeParser)
    {
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} != {fieldName}");
        }
        return ($"({string.Join(" && ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNumber(string fieldName, double[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} != {fieldName}");
        }
        return ($"({string.Join(" && ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlGuid(string fieldName, Guid[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} != {fieldName}");
        }
        return ($"({string.Join(" && ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableGuid(string fieldName, Guid[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} != {fieldName}");
        }
        return ($"({string.Join(" || ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDate(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token}.date != {fieldName}.date");
        }
        return ($"({string.Join(" && ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDate(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token}.date != {fieldName}.Value.date");
        }
        return ($"({string.Join(" && ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} != {fieldName}");
        }
        return ($"({string.Join(" && ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} != {fieldName}.Value");
        }
        return ($"({string.Join(" && ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
}