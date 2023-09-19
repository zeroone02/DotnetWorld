using DotnetWorld.DDD.Application;

namespace DotnetWorld.DDD.Application.Contracts;
public interface ICrudAppService<TDto, TKey, TCreateDto, TUpdateDto, TPagedRequestDto> : 
    ICreateAppService<TDto, TCreateDto>,
    IUpdateAppService<TDto, TKey, TUpdateDto>,
    IDeleteAppService<TKey>,
    IReadOnlyAppService<TDto, TKey, TPagedRequestDto> where TPagedRequestDto: PagedRequestDto
{

}
