namespace DotnetWorld.DDD.Application.Contracts;
public class PagedResultDto<TEntity>
{
    public PagedResultDto()
    {

    }
    public PagedResultDto(IReadOnlyCollection<TEntity> items)
    {
        Items = items;
    }

    public IReadOnlyCollection<TEntity> Items { get; set; }
}
