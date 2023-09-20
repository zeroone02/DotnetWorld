using DotnetWorld.WebService.Domain;
using static DotnetWorld.WebService.Domain.SD;

namespace DotnetWorld.WebService.Application.Contracts;
public class RequestDto<TData>
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public TData Data { get; set; }
    public string AccessToken { get; set; }
}
