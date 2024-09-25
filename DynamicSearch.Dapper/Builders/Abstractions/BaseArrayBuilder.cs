namespace DynamicSearch.Dapper.Builder.Abstraction;

public abstract class BaseArrayBuilder : IOperationBuilder
{
    private IValueArrayParser<string> _stringParser;
    private IValueArrayParser<double> _numbericParser;
    private IValueArrayParser<Guid> _guidArrayParser;
    private IValueArrayParser<DateTime> _dateTimeParser;

    protected abstract string Operation { get; }
    protected IDictionary<QueryType, OperationBuilder> SupportOperations;

    public BaseArrayBuilder(IValueArrayParser<string> stringParser = null,
                            IValueArrayParser<double> numbericParser = null,
                            IValueArrayParser<Guid> guidArrayParser = null,
                            IValueArrayParser<DateTime> dateTimeParser = null)
    {
        _stringParser = stringParser;
        _numbericParser = numbericParser;
        _guidArrayParser = guidArrayParser;
        _dateTimeParser = dateTimeParser;

        SupportOperations = new Dictionary<QueryType, OperationBuilder>
            {
                { QueryType.TEXT, BuildText },
                { QueryType.NUMBER, BuildNumber },
                { QueryType.GUID, BuildGuid },
                { QueryType.DATE, BuildDate },
                { QueryType.DATETIME, BuildDateTime }
            };
    }

    public virtual (string Query, object[] Values, string[] Tokens) Build(QueryFilter filter, Action<string[]> callback = null)
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
        var values = _stringParser.Parse(filter.QueryValue);
        callback?.Invoke(values);
        return BuildOperationSqlText(filter.QueryKey, values);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlText(string fieldName, string[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName} = {token}");
            listToken.Add(token);
        }
        return ($" ( {string.Join(" or ", listQuery.ToArray())} )", values, listToken.ToArray());
    }
    #endregion

    #region Number
    protected (string Query, object[] Values, string[] Tokens) BuildNumber(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_numbericParser == null)
            throw new InvalidOperationException($"{nameof(_numbericParser)} is null");
        var values = _numbericParser.Parse(filter.QueryValue);
        callback?.Invoke(values.Select(x => $"{x}").ToArray());
        return BuildOperationSqlNumber(filter.QueryKey, values);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlNumber(string fieldName, double[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName} = {token}");
            listToken.Add(token);
        }
        return ($"( {string.Join(" or ", listQuery.ToArray())} )", values.Select(x => x as object).ToArray(), listToken.ToArray());
    }
    #endregion

    #region Guid
    protected (string Query, object[] Values, string[] Tokens) BuildGuid(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_guidArrayParser == null)
            throw new InvalidOperationException($"{nameof(_guidArrayParser)} is null");
        var values = _guidArrayParser.Parse(filter.QueryValue);
        return BuildOperationSqlGuid(filter.QueryKey, values);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlGuid(string fieldName, Guid[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName} = {token}");
            listToken.Add(token);
        }
        return ($"( {string.Join(" or ", listQuery.ToArray())} )", values.Select(x => x as object).ToArray(), listToken.ToArray());
    }
    #endregion

    #region Date
    protected (string Query, object[] Values, string[] Tokens) BuildDate(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_dateTimeParser == null)
            throw new InvalidOperationException($"{nameof(_dateTimeParser)} is null");
        var values = _dateTimeParser.Parse(filter.QueryValue);
        return BuildOperationSqlDate(filter.QueryKey, values);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlDate(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName}::date = {token}");
            listToken.Add(token);
        }
        return ($"( {string.Join(" or ", listQuery.ToArray())} )", values.Select(x => x as object).ToArray(), listToken.ToArray());
    }
    #endregion

    #region DateTime
    protected (string Query, object[] Values, string[] Tokens) BuildDateTime(QueryFilter filter, Action<string[]> callback = null)
    {
        if (_dateTimeParser == null)
            throw new InvalidOperationException($"{nameof(_dateTimeParser)} is null");
        var values = _dateTimeParser.Parse(filter.QueryValue);
        return BuildOperationSqlDateTime(filter.QueryKey, values);
    }

    protected virtual (string Query, object[] Values, string[] Tokens) BuildOperationSqlDateTime(string fieldName, DateTime[] values)
    {
        var listToken = new List<string>();
        var listQuery = new List<string>();
        for (int i = 0; i < values.Length; i++)
        {
            var token = $"@value{i}";
            listQuery.Add($"{fieldName} = {token}");
            listToken.Add(token);
        }
        return ($"( {string.Join(" or ", listQuery.ToArray())} )", values.Select(x => x as object).ToArray(), listToken.ToArray());
    }
    #endregion
}