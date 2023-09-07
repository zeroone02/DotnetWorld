using System.ComponentModel.DataAnnotations;

namespace DotnetWorld.DDD.Application.Contracts;
public class PagedRequestDto
{
    [Range(0, int.MaxValue)]
    public int SkipCount { get; set; } = 0;

    [Range(0, int.MaxValue)]
    public int MaxResultCount { get; set; } = 10;
}
