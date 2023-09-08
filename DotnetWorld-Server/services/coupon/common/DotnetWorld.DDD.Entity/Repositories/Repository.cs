using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetWorld.DDD;
public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    private readonly IEfCoreDbContext _dbContext;
    public IServiceProvider ServiceProvider { get; }
    public Repository(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        _dbContext = ServiceProvider.GetService<IEfCoreDbContext>();
    }
    public Task DeleteAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<TEntity> GetAsync(TKey id)
    {

        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        return entity;
    }

    public async Task<List<TEntity>> GetListAsync(int skip, int take)
    {
        var list = await _dbContext.Set<TEntity>().Skip(skip).Take(take).ToListAsync();
        return list;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await _dbContext.Set<TEntity>().AddAsync(entity);
        return entry.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = _dbContext.Update(entity);
        return await Task.FromResult(entry.Entity);
    }
}
