namespace DotnetWorld.DDD.Application.Contracts;
public interface IReadOnlyAppService<TDto, TKey, TPagedRequestDto>
    where TPagedRequestDto : PagedRequestDto
{
    Task<TDto> GetAsync(TKey id);
    Task<PagedResultDto<TDto>> GetListAsync(TPagedRequestDto input);
}
