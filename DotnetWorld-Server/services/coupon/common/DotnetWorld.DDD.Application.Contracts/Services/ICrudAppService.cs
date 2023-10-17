using DotnetWorld.DDD.Application;

namespace DotnetWorld.DDD.Application.Contracts;
public interface ICrudAppService<TDto, TKey, TCreateDto, TUpdateDto> : 
    ICreateAppService<TDto, TCreateDto>,
    IUpdateAppService<TDto, TUpdateDto>,
    IDeleteAppService<TKey>,
    IReadOnlyAppService<TDto, TKey>
{

}
