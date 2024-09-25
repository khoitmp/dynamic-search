namespace DynamicSearch.Dapper.Builder.Abstraction;

public abstract class BaseBuilder : IOperationBuilder
{
    private IValueParser<string> _stringParser;
    private IValueParser<double> _numbericParser;
    private IValueParser<bool> _boolParser;
    private IValueParser<DateTime> _dateTimeParser;
    private IValueParser<Guid> _guidParser;

    protected abstract string Operation { get; }
    protected IDictionary<QueryType, OperationBuilder> SupportOperations;

    public BaseBuilder(IValueParser<string> stringParser = null,
                    IValueParser<double> numbericParser = null,
                    IValueParser<bool> boolParser = null,
                    IValueParser<Guid> guidParser = null,
                    IValueParser<DateTime> dateTimeParser = null)
    {
        _stringParser = stringParser;
        _numbericParser = numbericParser;
        _boolParser = boolParser;
        _guidParser = guidParser;
        _dateTimeParser = dateTimeParser;

        SupportOperations = new Dictionary<QueryType, OperationBuilder>
            {
                { QueryType.TEXT, BuildText },
                { QueryType.NUMBER, BuildNumber },
                { QueryType.BOOLEAN, BuildBoolean },
                { QueryType.GUID, BuildGuid },
                { QueryType.DATE, BuildDate },
                { QueryType.DATETIME, BuildDateTime }
            };
    }

    public (string Query, object[] Values, string[] Tokens) Build(QueryFilter filter, Action<string[]> callback = null)
    {
        if (!SupportOperations.ContainsKey(filter.QueryType))
            throw new NotSupportedException($"{filter.QueryType} is not support for {Operation} operation");
        return SupportOperations[filter.QueryType].Invoke(filter, callback);
    }

    #region Text
    protected (string Query, object[] Values, string[] Tokens) BuildText(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_stringParser == null)
            throw new InvalidOperationException($"{nameof(_stringParser)} is null");
        var value = _stringParser.Parse(filter.QueryValue);
        return BuildOperationSqlText(filter.QueryKey, value);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlText(string fieldName, string value)
    {
        var token = "@value";
        var query = $"{fieldName} = ${token}";
        return (query, new object[] { value }, new string[] { token });
    }
    #endregion

    #region Number
    protected (string Query, object[] Values, string[] Tokens) BuildNumber(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_numbericParser == null)
            throw new InvalidOperationException($"{nameof(_numbericParser)} is null");
        var value = _numbericParser.Parse(filter.QueryValue);
        return BuildOperationSqlNumber(filter.QueryKey, value);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlNumber(string fieldName, double value)
    {
        var token = "@value";
        var query = $"{fieldName} = ${token}";
        return (query, new object[] { value }, new string[] { token });
    }
    #endregion

    #region Boolean
    protected (string Query, object[] Values, string[] Tokens) BuildBoolean(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_boolParser == null)
            throw new InvalidOperationException($"{nameof(_boolParser)} is null");
        var value = _boolParser.Parse(filter.QueryValue);
        return BuildOperationSqlBoolean(filter.QueryKey, value);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlBoolean(string fieldName, bool value)
    {
        var token = "@value";
        var query = $"{fieldName} = {token}";
        return (query, new object[] { value }, new string[] { token });
    }
    #endregion

    #region Guid
    protected (string Query, object[] Values, string[] Tokens) BuildGuid(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_guidParser == null)
            throw new InvalidOperationException($"{nameof(_guidParser)} is null");
        var value = _guidParser.Parse(filter.QueryValue);
        return BuildOperationSqlGuid(filter.QueryKey, value);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlGuid(string fieldName, Guid value)
    {
        var token = "@value";
        var query = $"{fieldName} = {token}";
        return (query, new object[] { value }, new string[] { token });
    }
    #endregion

    #region Date
    protected (string Query, object[] Values, string[] Tokens) BuildDate(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_dateTimeParser == null)
            throw new InvalidOperationException($"{nameof(_dateTimeParser)} is null");
        var value = _dateTimeParser.Parse(filter.QueryValue);
        return BuildOperationSqlDate(filter.QueryKey, value);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlDate(string fieldName, DateTime value)
    {
        var token = "@value";
        var query = $"{fieldName}::date = {token}";
        return (query, new object[] { value }, new string[] { token });
    }
    #endregion

    #region DateTime
    protected (string Query, object[] Values, string[] Tokens) BuildDateTime(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_dateTimeParser == null)
            throw new InvalidOperationException($"{nameof(_dateTimeParser)} is null");
        var value = _dateTimeParser.Parse(filter.QueryValue);
        return BuildOperationSqlDateTime(filter.QueryKey, value);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlDateTime(string fieldName, DateTime value)
    {
        var token = "@value";
        var query = $"{fieldName} = {token}";
        return (query, new object[] { value }, new string[] { token });
    }
    #endregion
}