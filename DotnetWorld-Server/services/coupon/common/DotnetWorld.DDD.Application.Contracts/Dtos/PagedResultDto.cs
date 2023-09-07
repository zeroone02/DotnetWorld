namespace DotnetWorld.DDD.Application.Contracts;
public class PagedResultDto<TEntity>
{
    public PagedResultDto()
    {

    }
    public PagedResultDto(IReadOnlyCollection<TEntity> items, long totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public IReadOnlyCollection<TEntity> Items { get; set; }
    public long TotalCount { get; set; }
}
