namespace DynamicSearch.Dapper.Interface;

public interface IQueryService
{
    (string Query, ExpandoObject Value) CompileQuery(string query, QueryCriteria queryCriteria, bool paging = true);
}