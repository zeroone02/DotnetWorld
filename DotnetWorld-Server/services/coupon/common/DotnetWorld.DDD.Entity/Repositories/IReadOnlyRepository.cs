namespace DotnetWorld.DDD;
public interface IReadOnlyRepository<TEntity, TKey>
{
    Task<TEntity> GetAsync(TKey id);
    Task<List<TEntity>> GetListAsync(int skip, int take);
}
