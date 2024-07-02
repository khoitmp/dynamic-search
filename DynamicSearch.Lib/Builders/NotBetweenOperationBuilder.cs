namespace DynamicSearch.Lib.Service;

internal class NotBetweenOperationBuilder : BetweenOperationBuilder
{
    protected override string Operation => Operations.NOT_BETWEEN;

    public NotBetweenOperationBuilder(IValueArrayParser<double> numbericParser,
                                        IValueArrayParser<DateTime> dateTimeParser)
        : base(numbericParser: numbericParser, dateTimeParser: dateTimeParser)
    {
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDate(string fieldName, DateTime[] values)
    {
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        string tokenFrom = "@valueFrom", tokenTo = "@valueTo";
        string query = $"({fieldName}.Date < {tokenFrom}.Date || {fieldName}.Date > {tokenTo}.Date)";
        return (query, new string[] { tokenFrom, tokenTo }, values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDate(string fieldName, DateTime[] values)
    {
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        string tokenFrom = "@valueFrom", tokenTo = "@valueTo";
        string query = $"({fieldName}.Value.Date < {tokenFrom}.Value.Date || {fieldName}.Value.Date > {tokenTo}.Value.Date)";
        return (query, new string[] { tokenFrom, tokenTo }, values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime(string fieldName, DateTime[] values)
    {
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        string tokenFrom = "@valueFrom", tokenTo = "@valueTo";
        string query = $"({fieldName} < {tokenFrom} || {fieldName} > {tokenTo})";
        return (query, new string[] { tokenFrom, tokenTo }, values.Select(v => v as object).ToArray());
    }

    protected override (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime(string fieldName, DateTime[] values)
    {
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        string tokenFrom = "@valueFrom", tokenTo = "@valueTo";
        string query = $"({fieldName}.Value < {tokenFrom}.Value || {fieldName}.Value > {tokenTo}.Value)";
        return (query, new string[] { tokenFrom, tokenTo }, values.Select(v => v as object).ToArray());
    }
}