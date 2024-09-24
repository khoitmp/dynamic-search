namespace DynamicSearch.EfCore.Service;

internal class BetweenOperationBuilder : BaseArrayBuilder
{
    protected override string Operation => Operations.BETWEEN;

    public BetweenOperationBuilder(IValueArrayParser<double> numbericParser,
                                   IValueArrayParser<DateTime> dateTimeParser)
        : base(numbericParser: numbericParser, dateTimeParser: dateTimeParser)
    {
        SupportedOperations.Clear();
        SupportedOperations.Add(QueryType.DATE, BuildDate);
        SupportedOperations.Add(QueryType.NULLABLE_DATE, BuildNullableDate);
        SupportedOperations.Add(QueryType.DATETIME, BuildDateTime);
        SupportedOperations.Add(QueryType.NULLABLE_DATETIME, BuildNullableDateTime);
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDate(string fieldName, DateTime[] values)
    {
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        string tokenFrom = "@valueFrom", tokenTo = "@valueTo";
        string query = $"({tokenFrom}.Date <= {fieldName}.Date && {fieldName}.Date <= {tokenTo}.Date)";
        return (query, new string[] { tokenFrom, tokenTo }, values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDate(string fieldName, DateTime[] values)
    {
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        string tokenFrom = "@valueFrom", tokenTo = "@valueTo";
        string query = $"({tokenFrom}.Value.Date <= {fieldName}.Value.Date && {fieldName}.Value.Date <= {tokenTo}.Value.Date)";
        return (query, new string[] { tokenFrom, tokenTo }, values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime(string fieldName, DateTime[] values)
    {
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        string tokenFrom = "@valueFrom", tokenTo = "@valueTo";
        string query = $"({tokenFrom} <= {fieldName} && {fieldName} <= {tokenTo})";
        return (query, new string[] { tokenFrom, tokenTo }, values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime(string fieldName, DateTime[] values)
    {
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        string tokenFrom = "@valueFrom", tokenTo = "@valueTo";
        string query = $"({tokenFrom}.Value <= {fieldName}.Value && {fieldName}.Value <= {tokenTo}.Value)";
        return (query, new string[] { tokenFrom, tokenTo }, values.Select(v => v as object).ToArray());
    }
}