using DotnetWorld.DDD;

namespace DotnetWorld.WebService.Application.Contracts;
public interface IBaseService<TRequestData,TResponseData>
{
    ResponseDto<TResponseData?> SendAsync(RequestDto<TRequestData> requestDto);
}
