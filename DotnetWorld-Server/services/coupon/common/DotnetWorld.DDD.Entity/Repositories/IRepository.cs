using DotnetWorld.DDD.Entities;

namespace DotnetWorld.DDD.Repositories;
public interface IRepository<TEntity, TKey> 
    : IReadOnlyRepository<TEntity, TKey>
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TKey id);
}
