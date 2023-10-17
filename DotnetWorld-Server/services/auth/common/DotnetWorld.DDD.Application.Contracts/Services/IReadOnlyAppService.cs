namespace DotnetWorld.DDD.Application.Contracts;
public interface IReadOnlyAppService<TDto, TKey>
    
{
    Task<TDto> GetAsync(TKey id);
    Task<List<TDto>> GetListAsync();
}
