namespace DynamicSearch.EfCore.Service;

public abstract class BaseSearchService<TEntity, TKey, TCriteria, TResponse> : ISearchService<TEntity, TKey, TCriteria, TResponse>, IFetchService<TEntity, TKey, TResponse>
        where TCriteria : BaseSearchCriteria
        where TEntity : class, IEntity<TKey>
        where TResponse : class, new()
{
    private readonly IServiceProvider _serviceProvider;

    protected Func<TEntity, TResponse> ConvertToModel { get; private set; }

    public BaseSearchService(IServiceProvider serviceProvider, Func<TEntity, TResponse> convertToModel)
    {
        _serviceProvider = serviceProvider;
        ConvertToModel = convertToModel;
    }

    public virtual async Task<BaseSearchResponse<TResponse>> SearchAsync(TCriteria criteria)
    {
        var start = DateTime.UtcNow;
        var data = new List<TResponse>();
        var response = BaseSearchResponse<TResponse>.CreateFrom(criteria, 0, 0, data);
        var tasks = new Task[]{
                RetrieveDataAsync(criteria, response),
                CountAsync(criteria, response)
            };

        await Task.WhenAll(tasks);

        var totalMilisecond = DateTime.UtcNow.Subtract(start).TotalMilliseconds;
        response.DurationInMilisecond = (long)totalMilisecond;

        return response;
    }

    public virtual Task<BaseSearchResponse<TResponse>> SearchAsync(TCriteria criteria, IList<FilterCriteria> additionalFilters)
    {
        var filterArray = new JArray();
        var originFilter = criteria.Filter;

        if (originFilter != null)
        {
            var originFilterOperation = criteria.Filter.Property(FilterOperations.AND);

            if (originFilterOperation != null)
            {
                var originFilterValues = (JArray)originFilterOperation.Value;
                foreach (var filterValue in originFilterValues)
                {
                    filterArray.Add(filterValue);
                }
            }
            else
            {
                filterArray.Add(originFilter);
            }
        }

        foreach (var additionalFilter in additionalFilters)
        {
            filterArray.Add(JObject.FromObject(additionalFilter));
        }

        if (filterArray.Any())
        {
            var newFilter = new JObject();

            newFilter[FilterOperations.AND] = filterArray;

            criteria.Filter = newFilter;
        }

        return SearchAsync(criteria);
    }

    protected virtual async Task RetrieveDataAsync(TCriteria criteria, BaseSearchResponse<TResponse> result)
    {
        var serviceScopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var dbType = GetDbType();
            var repository = scope.ServiceProvider.GetService(dbType) as ISearchRepository<TEntity, TKey>;

            if (repository == null)
            {
                throw new Exception("The repository must be implemented the ISearchRepository");
            }

            var query = BuildQuery(repository, criteria);
            var data = await query.Skip(criteria.PageIndex * criteria.PageSize).Take(criteria.PageSize).ToListAsync();

            if (data.Any())
            {
                result.AddRangeData(data.Select(ConvertToModel));
            }
        }
    }

    protected virtual async Task CountAsync(TCriteria criteria, BaseSearchResponse<TResponse> result)
    {
        var serviceScopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var dbType = GetDbType();
            var repository = scope.ServiceProvider.GetService(dbType) as ISearchRepository<TEntity, TKey>;

            if (repository == null)
            {
                throw new Exception("The repository must be implemented the ISearchRepository");
            }

            var query = BuildQuery(repository, criteria);

            result.TotalCount = await query.CountAsync();
        }
    }

    protected virtual IQueryable<TEntity> BuildQuery(ISearchRepository<TEntity, TKey> repository, TCriteria criteria)
    {
        var query = repository.AsQueryable().AsNoTracking();
        return BuildQuery(query, criteria);
    }

    protected virtual IQueryable<TEntity> BuildQuery(IQueryable<TEntity> query, TCriteria criteria)
    {
        var count = 0;
        var compiler = _serviceProvider.GetRequiredService<IQueryCompiler>();

        if (criteria.Filter != null)
        {
            var rs = compiler.Compile(criteria.Filter, ref count);
            query = query.Where(rs.Query, rs.Values);
        }

        if (!string.IsNullOrEmpty(criteria.Sorts))
        {
            var orders = criteria.Sorts.Split(';').Select(s => s.Replace("=", " "));
            query = query.OrderBy(string.Join(", ", orders));
        }

        if (criteria.Fields != null && criteria.Fields.Any())
        {
            query = query.Select<TEntity>($"new ({string.Join(",", criteria.Fields)})");
        }

        return query;
    }

    public virtual async Task<TResponse> FetchAsync(TKey id)
    {
        var dbType = GetDbType();
        var fetchRepository = _serviceProvider.GetService(dbType) as IFetchRepository<TEntity, TKey>;

        if (fetchRepository == null)
        {
            throw new Exception("The repository must be implemented the IFetchRepository");
        }

        var entity = await fetchRepository.AsFetchable().Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        if (entity != null)
        {
            return ConvertToModel(entity);
        }

        return null;
    }

    protected abstract Type GetDbType();
}