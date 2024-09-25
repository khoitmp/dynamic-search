namespace DynamicSearch.Dapper.Model;

public class QueryCriteria
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public JObject Filter { get; set; }
    public string Sorts { get; set; }
}