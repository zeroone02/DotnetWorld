using DotnetWorld.DDD;

namespace DotnetWorld.WebService.Application.Contracts;
public interface IBaseService
{
    Task<ResponseDto>? SendAsync(RequestDto requestDto);
}
