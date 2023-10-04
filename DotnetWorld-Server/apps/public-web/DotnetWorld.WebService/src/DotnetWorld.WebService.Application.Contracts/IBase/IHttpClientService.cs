using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;

namespace DotnetWorld.WebService.Application.Contracts;
public interface IHttpClientService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto);
}
