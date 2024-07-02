namespace DynamicSearch.Lib.Service;

internal abstract class BaseArrayBuilder : IOperationBuilder
{
    private IValueArrayParser<string> _stringParser;
    private IValueArrayParser<double> _numbericParser;
    private IValueArrayParser<Guid> _guidArrayParser;
    private IValueArrayParser<DateTime> _dateTimeParser;

    protected abstract string Operation { get; }
    protected IDictionary<QueryType, OperationBuilder> SupportedOperations;

    public BaseArrayBuilder(IValueArrayParser<string> stringParser = null,
                            IValueArrayParser<double> numbericParser = null,
                            IValueArrayParser<Guid> guidArrayParser = null,
                            IValueArrayParser<DateTime> dateTimeParser = null)
    {
        _stringParser = stringParser;
        _numbericParser = numbericParser;
        _guidArrayParser = guidArrayParser;
        _dateTimeParser = dateTimeParser;

        SupportedOperations = new Dictionary<QueryType, OperationBuilder>
            {
                { QueryType.TEXT, BuildText },
                { QueryType.NUMBER, BuildNumber },
                { QueryType.GUID, BuildGuid },
                { QueryType.NULLABLE_GUID, BuildNullableGuid },
                { QueryType.DATE, BuildDate },
                { QueryType.NULLABLE_DATE, BuildNullableDate },
                { QueryType.DATETIME, BuildDateTime },
                { QueryType.NULLABLE_DATETIME, BuildNullableDateTime }
            };
    }

    public virtual (string Query, string[] Tokens, object[] Values) Build(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (!SupportedOperations.ContainsKey(filter.QueryType)) throw new NotSupportedException($"{filter.QueryType} is not support for {Operation} operation");
        return SupportedOperations[filter.QueryType].Invoke(filter, callback);
    }

    #region Text
    protected (string Query, string[] Tokens, object[] Values) BuildText(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_stringParser == null) throw new InvalidOperationException($"{nameof(_stringParser)} is null");
        var values = _stringParser.Parse(filter.QueryValue);
        callback?.Invoke(values);
        return BuildOperationSqlText(filter.QueryKey, values);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} == {fieldName}");
        }
        return ($"({string.Join(" || ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
    #endregion

    #region Number
    protected (string Query, string[] Tokens, object[] Values) BuildNumber(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_numbericParser == null) throw new InvalidOperationException($"{nameof(_numbericParser)} is null");
        var values = _numbericParser.Parse(filter.QueryValue);
        callback?.Invoke(values.Select(v => $"{v}").ToArray());
        return BuildOperationSqlNumber(filter.QueryKey, values);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNumber(string fieldName, double[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} == {fieldName}");
        }
        return ($"({string.Join(" || ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
    #endregion

    #region Guid
    protected (string Query, string[] Tokens, object[] Values) BuildGuid(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_guidArrayParser == null) throw new InvalidOperationException($"{nameof(_guidArrayParser)} is null");
        var values = _guidArrayParser.Parse(filter.QueryValue);
        return BuildOperationSqlGuid(filter.QueryKey, values);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlGuid(string fieldName, Guid[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} == {fieldName}");
        }
        return ($"({string.Join(" || ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
    #endregion

    #region Nullable Guid
    protected (string Query, string[] Tokens, object[] Values) BuildNullableGuid(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_guidArrayParser == null) throw new InvalidOperationException($"{nameof(_guidArrayParser)} is null");
        var values = _guidArrayParser.Parse(filter.QueryValue);
        return BuildOperationSqlNullableGuid(filter.QueryKey, values);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableGuid(string fieldName, Guid[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token}.Value == {fieldName}.Value");
        }
        return ($"({string.Join("||", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
    #endregion

    #region Date
    protected (string Query, string[] Tokens, object[] Values) BuildDate(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_dateTimeParser == null) throw new InvalidOperationException($"{nameof(_dateTimeParser)} is null");
        var values = _dateTimeParser.Parse(filter.QueryValue);
        return BuildOperationSqlDate(filter.QueryKey, values);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlDate(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token}.Date == {fieldName}.Date");
        }
        return ($"({string.Join("||", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
    #endregion

    #region Nullable Date
    protected (string Query, string[] Tokens, object[] Values) BuildNullableDate(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_dateTimeParser == null) throw new InvalidOperationException($"{nameof(_dateTimeParser)} is null");
        var values = _dateTimeParser.Parse(filter.QueryValue);
        return BuildOperationSqlNullableDate(filter.QueryKey, values);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDate(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token}.Value.Date == {fieldName}.Value.Date");
        }
        return ($"({string.Join(" || ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
    #endregion

    #region DateTime
    protected (string Query, string[] Tokens, object[] Values) BuildDateTime(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_dateTimeParser == null) throw new InvalidOperationException($"{nameof(_dateTimeParser)} is null");
        var values = _dateTimeParser.Parse(filter.QueryValue);
        return BuildOperationSqlDateTime(filter.QueryKey, values);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token} == {fieldName}");
        }
        return ($"({string.Join(" || ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
    #endregion

    #region Nullable DateTime
    protected (string Query, string[] Tokens, object[] Values) BuildNullableDateTime(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_dateTimeParser == null) throw new InvalidOperationException($"{nameof(_dateTimeParser)} is null");
        var values = _dateTimeParser.Parse(filter.QueryValue);
        return BuildOperationSqlNullableDateTime(filter.QueryKey, values);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listToken.Add(token);
            listQuery.Add($"{token}.Value == {fieldName}.Value");
        }
        return ($"({string.Join(" || ", listQuery.ToArray())})", listToken.ToArray(), values.Select(v => v as object).ToArray());
    }
    #endregion
}