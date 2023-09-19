using AutoMapper;
using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.CouponService.Application;
public abstract class CrudAppService<TEntity, TDto, TKey, TCreateDto, TUpdateDto, TPagedRequestDto>
    : ICrudAppService<TDto, TKey, TCreateDto, TUpdateDto, TPagedRequestDto> where TPagedRequestDto : PagedRequestDto
{
    public CrudAppService(
        IServiceProvider serviceProvider,
        IMapper mapper)
    {
        ServiceProvider = serviceProvider;
        ObjectMapper = mapper;
    }
    protected IServiceProvider ServiceProvider { get; }
    protected IMapper ObjectMapper { get; }

    public abstract Task<TDto> CreateAsync(TCreateDto input);
    public abstract Task DeleteAsync(TKey id);
    public abstract Task<TDto> GetAsync(TKey id);
    public abstract Task<PagedResultDto<TDto>> GetListAsync(TPagedRequestDto input);
    public abstract Task<TDto> UpdateAsync(TKey id, TUpdateDto input);
}