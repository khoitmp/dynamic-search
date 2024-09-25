namespace DynamicSearch.Dapper;

/// <summary>
/// TODO: Update the service to prevent SQL Injection
/// </summary>
public class DapperQueryService : IQueryService
{
    private readonly IFilterCompiler _filterCompiler;

    public DapperQueryService(IFilterCompiler filterCompiler)
    {
        _filterCompiler = filterCompiler;
    }

    public (string Query, ExpandoObject Value) CompileQuery(string query, QueryCriteria queryCriteria, bool paging = true)
    {
        var queryBuilder = new StringBuilder();

        queryBuilder.Append(query);

        var filterQuery = string.Empty;
        var value = new ExpandoObject();

        if (queryCriteria.Filter != null)
        {
            int count = 0;
            (filterQuery, value) = _filterCompiler.Compile(queryCriteria.Filter, ref count);
        }

        if (!string.IsNullOrEmpty(filterQuery))
        {
            queryBuilder.Append($" where {filterQuery}");
        }

        var sortQuery = BuidSortQuery(queryCriteria.Sorts);
        if (!string.IsNullOrEmpty(sortQuery))
        {
            queryBuilder.Append($" order by {sortQuery}");
        }

        if (paging)
        {
            var pagingQuery = $"limit {queryCriteria.PageSize} offset {queryCriteria.PageIndex * queryCriteria.PageSize}";
            queryBuilder.Append($" {pagingQuery}");
        }

        var resultQuery = queryBuilder.ToString();

        return (resultQuery, value);
    }

    public string BuidSortQuery(string sorts)
    {
        var resultQuery = string.Empty;
        if (!string.IsNullOrEmpty(sorts))
        {
            var sortList = new List<string>();
            var splitedSorts = sorts.Split(",");
            foreach (var splitedSort in splitedSorts)
            {
                var splitedOrderBy = splitedSort.Split("=");
                if (splitedOrderBy.Count() == 2)
                {
                    var columnName = splitedOrderBy[0];
                    var orderByType = splitedOrderBy[1];
                    sortList.Add($"{columnName.ToStringQuote()} {orderByType}");
                }
            }
            resultQuery = string.Join(",", sortList);
        }
        return resultQuery;
    }
}