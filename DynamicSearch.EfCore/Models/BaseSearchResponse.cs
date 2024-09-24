namespace DynamicSearch.EfCore.Model;

public class BaseSearchResponse<T>
{
    private List<T> _data;

    public IEnumerable<T> Data => _data;
    public long DurationInMilisecond { get; set; }
    public int TotalCount { get; set; }
    public int TotalPage => (TotalCount / PageSize) + ((TotalCount % PageSize) > 0 ? 1 : 0);
    public int PageSize { get; set; }
    public int PageIndex { get; set; }

    public BaseSearchResponse()
    {
        _data = new List<T>();
    }

    public void AddRangeData(IEnumerable<T> data)
    {
        _data.AddRange(data);
    }

    public void Add(T item)
    {
        _data.Add(item);
    }

    public BaseSearchResponse(long duration, int totalCount, int pageSize, int pageIndex, IEnumerable<T> data)
    {
        _data = new List<T>(data);

        DurationInMilisecond = duration;
        TotalCount = totalCount;
        PageSize = pageSize;
        PageIndex = pageIndex;
    }

    public static BaseSearchResponse<T> CreateFrom(BaseSearchCriteria criteria, long duration, int totalCount, IEnumerable<T> data)
    {
        return new BaseSearchResponse<T>(duration, totalCount, criteria.PageSize, criteria.PageIndex, data);
    }
}