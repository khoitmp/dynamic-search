namespace DynamicSearch.EfCore.Interface;

public interface IFetchService<TEntity, TKey, TResponse>
    where TEntity : class, IEntity<TKey>
    where TResponse : class, new()
{
    public Task<TResponse> FetchAsync(TKey id);
}