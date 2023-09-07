namespace DotnetWorld.DDD.Application.Contracts;
public class ListResultDto<TEntity>
{
	public ListResultDto() { }
	public ListResultDto(IReadOnlyCollection<TEntity> items)
	{
		Items = items;
	}
    public IReadOnlyCollection<TEntity> Items { get; set; }
}
