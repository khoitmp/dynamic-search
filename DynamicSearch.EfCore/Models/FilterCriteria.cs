namespace DynamicSearch.EfCore.Model;

public class FilterCriteria
{
    public string QueryKey { get; set; }
    public QueryType QueryType { get; set; }
    public string QueryValue { get; set; }
    public string Operation { get; set; }

    public FilterCriteria(string queryKey, QueryType queryType, string queryValue, string operation)
    {
        QueryKey = queryKey;
        QueryType = queryType;
        QueryValue = queryValue;
        Operation = operation;
    }

    public static FilterCriteria CreateFrom(string queryKey, QueryType queryType, string queryValue, string operation)
    {
        return new FilterCriteria(queryKey, queryType, queryValue, operation);
    }
}