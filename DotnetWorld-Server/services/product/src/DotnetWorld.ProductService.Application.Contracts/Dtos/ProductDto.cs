using DotnetWorld.DDD.Application.Contracts;
using Microsoft.AspNetCore.Http;    

namespace DotnetWorld.ProductService.Application.Contracts;
 public class ProductDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageLocalPath { get; set; }
    public IFormFile? Image { get; set; }
}
