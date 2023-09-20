using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;

namespace DotnetWorld.WebService.Application;
public class BaseService : IBaseService
{
    public Task<ResponseDto>? SendAsync(RequestDto requestDto)
    {
        throw new NotImplementedException();
    }
}
