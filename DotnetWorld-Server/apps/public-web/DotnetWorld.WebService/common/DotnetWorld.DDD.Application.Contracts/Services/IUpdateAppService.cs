namespace DotnetWorld.DDD.Application.Contracts;
public interface IUpdateAppService<TDto, TKey, TUpdateDto>
{
    Task<TDto> UpdateAsync(TKey id, TUpdateDto input);
}
