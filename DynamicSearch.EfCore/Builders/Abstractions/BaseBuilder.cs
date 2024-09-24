namespace DynamicSearch.EfCore.Service.Abstraction;

internal abstract class BaseBuilder : IOperationBuilder
{
    private IValueParser<string> _stringParser;
    private IValueParser<double> _numbericParser;
    private IValueParser<bool> _boolParser;
    private IValueParser<DateTime> _dateParser;
    private IValueParser<Guid> _guidParser;

    protected abstract string Operation { get; }
    protected IDictionary<QueryType, OperationBuilder> SupportedQueryTypes;

    public BaseBuilder(IValueParser<string> stringParser = null,
                        IValueParser<double> numbericParser = null,
                        IValueParser<bool> boolParser = null,
                        IValueParser<Guid> guidParser = null,
                        IValueParser<DateTime> dateParser = null)
    {
        _stringParser = stringParser;
        _numbericParser = numbericParser;
        _boolParser = boolParser;
        _guidParser = guidParser;
        _dateParser = dateParser;

        SupportedQueryTypes = new Dictionary<QueryType, OperationBuilder>
            {
                { QueryType.TEXT, BuildText },
                { QueryType.NUMBER, BuildNumber },
                { QueryType.BOOLEAN, BuildBoolean },
                { QueryType.GUID, BuildGuid },
                { QueryType.NULLABLE_GUID, BuildNullableGuid },
                { QueryType.DATE, BuildDate },
                { QueryType.NULLABLE_DATE, BuildNullableDate },
                { QueryType.DATETIME, BuildDateTime },
                { QueryType.NULLABLE_DATETIME, BuildNullableDateTime }
            };
    }

    public (string Query, string[] Tokens, object[] Values) Build(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (!SupportedQueryTypes.ContainsKey(filter.QueryType)) throw new NotSupportedException($"{filter.QueryType} is not support for {Operation} operation");
        return SupportedQueryTypes[filter.QueryType].Invoke(filter, callback);
    }

    #region Text
    protected (string Query, string[] Tokens, object[] Values) BuildText(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_stringParser == null) throw new InvalidOperationException($"{nameof(_stringParser)} is null");
        var value = _stringParser.Parse(filter.QueryValue);
        return BuildOperationSqlText(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlText(string fieldName, string value)
    {
        var token = "@value";
        var query = $"{fieldName} == {token}";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion

    #region Number
    protected (string Query, string[] Tokens, object[] Values) BuildNumber(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_numbericParser == null) throw new InvalidOperationException($"{nameof(_numbericParser)} is null");
        var value = _numbericParser.Parse(filter.QueryValue);
        return BuildOperationSqlNumber(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNumber(string fieldName, double value)
    {
        var token = "@value";
        var query = $"{fieldName} == {token}";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion

    #region Boolean
    protected (string Query, string[] Tokens, object[] Values) BuildBoolean(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_boolParser == null) throw new InvalidOperationException($"{nameof(_boolParser)} is null");
        var value = _boolParser.Parse(filter.QueryValue);
        return BuildOperationSqlBoolean(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlBoolean(string fieldName, bool value)
    {
        var token = "@value";
        var query = $"{fieldName} == {token}";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion

    #region Guid
    protected (string Query, string[] Tokens, object[] Values) BuildGuid(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_guidParser == null) throw new InvalidOperationException($"{nameof(_guidParser)} is null");
        var value = _guidParser.Parse(filter.QueryValue);
        return BuildOperationSqlGuid(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlGuid(string fieldName, Guid value)
    {
        var token = "@value";
        var query = $"{fieldName} == {token}";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion

    #region Nullable Guid
    protected (string Query, string[] Tokens, object[] Values) BuildNullableGuid(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_guidParser == null) throw new InvalidOperationException($"{nameof(_guidParser)} is null");
        var value = _guidParser.Parse(filter.QueryValue);
        return BuildOperationSqlNullableGuid(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableGuid(string fieldName, Guid value)
    {
        var token = "@value";
        var query = $"{fieldName}.Value == {token}.Value";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion

    #region Date
    protected (string Query, string[] Tokens, object[] Values) BuildDate(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_dateParser == null) throw new InvalidOperationException($"{nameof(_dateParser)} is null");
        var value = _dateParser.Parse(filter.QueryValue);
        return BuildOperationSqlDate(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlDate(string fieldName, DateTime value)
    {
        var token = "@value";
        var query = $"{fieldName}.Date == {token}.Date";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion

    #region Nullable Date
    protected (string Query, string[] Tokens, object[] Values) BuildNullableDate(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_dateParser == null) throw new InvalidOperationException($"{nameof(_dateParser)} is null");
        var value = _dateParser.Parse(filter.QueryValue);
        return BuildOperationSqlNullableDate(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDate(string fieldName, DateTime value)
    {
        var token = "@value";
        var query = $"{fieldName}.Value.Date == {token}.Value.Date";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion

    #region DateTime
    protected (string Query, string[] Tokens, object[] Values) BuildDateTime(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_dateParser == null) throw new InvalidOperationException($"{nameof(_dateParser)} is null");
        var value = _dateParser.Parse(filter.QueryValue);
        return BuildOperationSqlDateTime(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlDateTime(string fieldName, DateTime value)
    {
        var token = "@value";
        var query = $"{fieldName} == {token}";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion

    #region Nullable DateTime
    protected (string Query, string[] Tokens, object[] Values) BuildNullableDateTime(FilterCriteria filter, Action<string[]> callback = null)
    {
        if (_dateParser == null) throw new InvalidOperationException($"{nameof(_dateParser)} is null");
        var value = _dateParser.Parse(filter.QueryValue);
        return BuildOperationSqlNullableDateTime(filter.QueryKey, value);
    }

    protected virtual (string Query, string[] Tokens, object[] Values) BuildOperationSqlNullableDateTime(string fieldName, DateTime value)
    {
        var token = "@value";
        var query = $"{fieldName}.Value == {token}.Value";
        return (query, new string[] { token }, new object[] { value });
    }
    #endregion
}