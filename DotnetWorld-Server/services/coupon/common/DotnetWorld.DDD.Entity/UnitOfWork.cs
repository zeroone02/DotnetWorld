using Microsoft.Extensions.DependencyInjection;
namespace DotnetWorld.DDD;
public class UnitOfWork
{
    private readonly IEfCoreDbContext _dbContext;
    public IServiceProvider ServiceProvider { get; }
    public UnitOfWork(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        _dbContext = ServiceProvider.GetService<IEfCoreDbContext>();
    }

    public async  Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
