namespace DotnetWorld.DDD.Application.Contracts;
public interface IUpdateAppService<TDto,TUpdateDto>
{
    Task<TDto> UpdateAsync(TUpdateDto input);
}
