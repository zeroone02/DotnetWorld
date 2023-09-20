using DotnetWorld.WebService.Domain;
using static DotnetWorld.WebService.Domain.SD;

namespace DotnetWorld.WebService.Application.Contracts;
public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
}
