namespace DynamicSearch.EfCore.Interface;

public interface ISearchService<TEntity, TKey, TCriteria, TResponse>
        where TCriteria : BaseSearchCriteria
        where TEntity : class, IEntity<TKey>
        where TResponse : class, new()
{
    Task<BaseSearchResponse<TResponse>> SearchAsync(TCriteria criteria);
    Task<BaseSearchResponse<TResponse>> SearchAsync(TCriteria criteria, IList<FilterCriteria> additionalFilters);
}