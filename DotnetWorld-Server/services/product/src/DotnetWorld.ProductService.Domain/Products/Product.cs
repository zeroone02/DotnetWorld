
using DotnetWorld.DDD;
using System.ComponentModel.DataAnnotations;

namespace DotnetWorld.ProductService.Domain;
public class Product : Entity<Guid>
{
    [Required]
    public string Name { get; set; }
    [Range(1, 10000)]
    public double Price { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageLocalPath { get; set; }
}
