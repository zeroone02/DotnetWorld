namespace DotnetWorld.DDD.Application.Contracts;
public interface IDeleteAppService<TKey>
{
    Task DeleteAsync(TKey id);
}
