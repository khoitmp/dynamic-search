namespace DynamicSearch.Lib.Service;

internal class AgoOperationBuilder : IOperationBuilder
{
    protected string Operation = Operations.AGO;
    protected IDictionary<QueryType, OperationBuilder> SupportedOperations;

    public AgoOperationBuilder()
    {
        SupportedOperations = new Dictionary<QueryType, OperationBuilder>
            {
                { QueryType.DATETIME2, BuildDateTime2 },
                { QueryType.NULLABLE_DATETIME2, BuildNullableDateTime2 }
            };
    }

    public (string Query, string[] Tokens, object[] Values) Build(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (!SupportedOperations.ContainsKey(filter.QueryType)) throw new NotSupportedException($"{filter.QueryType} is not support for {Operation} operation");
        return SupportedOperations[filter.QueryType].Invoke(filter, callback);
    }

    #region DateTime2
    protected (string Query, string[] Tokens, object[] Values) BuildDateTime2(FilterCriteria filter, Action<string[]> callback = null)
    {
        var values = filter.QueryValue.TrimStart('[').TrimEnd(']').Split(',');
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        var compareFieldName = values[0];
        var value = int.Parse(values[1]);
        if (values[0].Equals("now()"))
        {
            compareFieldName = "DateTime.Now";
        }
        return BuildOperationSqlDateTime2(filter.QueryKey, compareFieldName, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime2(string fieldName, string compareFieldName, int value)
    {
        // TODO: Should check datetime not nullable
        var token = "@value";
        var query = $"({fieldName}.AddHours({token}) <= {compareFieldName} && {compareFieldName} <= {fieldName})";
        return (query, new string[] { token }, new object[] { -value });
    }
    #endregion

    #region Nullable DateTime2
    protected (string Query, string[] Tokens, object[] Values) BuildNullableDateTime2(FilterCriteria filter, Action<string[]> callback = null)
    {
        var values = filter.QueryValue.TrimStart('[').TrimEnd(']').Split(',');
        if (values.Length != 2) throw new ArgumentException("DateTime value should be an array of 2 values");
        var value = int.Parse(values[1]);
        var compareFieldName = values[0];
        if (values[0].Equals("now()"))
        {
            compareFieldName = "DateTime.Now";
        }
        return BuildOperationSqlNullableDateTime2(filter.QueryKey, compareFieldName, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime2(string fieldName, string compareFieldName, int value)
    {
        // TODO: Should check datetime not nullable
        var token = "@value";
        var query = $"({fieldName}.Value.AddHours(token) <= {compareFieldName} && {compareFieldName} <= {fieldName})";
        return (query, new string[] { token }, new object[] { -value });
    }
    #endregion
}