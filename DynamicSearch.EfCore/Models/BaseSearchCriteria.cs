namespace DynamicSearch.EfCore.Model;

public class BaseSearchCriteria
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 20;
    public JObject Filter { get; set; }
    public string Sorts { get; set; } = "updatedUtc=desc";
    public string[] Fields { get; set; }
}