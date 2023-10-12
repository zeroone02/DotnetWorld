using DotnetWorld.DDD;
using System.ComponentModel.DataAnnotations;

namespace DotnetWorld.ProductService.Domain;
public class Product : AggregateRoot<Guid>
{
    internal Product(Product product)
    {
        Id = product.Id;
        Price = product.Price;
        Description = product.Description;
        CategoryName = product.CategoryName;
        ImageUrl = product.ImageUrl;
        ImageLocalPath = product.ImageLocalPath;
    }
    internal Product(Guid productId, double price,string description,
        string categoryName,string imageUrl,string imageLocalPath)
    {
        Id = productId;
        Price = price;
        Description = description;
        CategoryName = categoryName;
        ImageUrl = imageUrl;
        ImageLocalPath = imageLocalPath;
    }
    internal Product(double price, string description,
       string categoryName, string imageUrl, string imageLocalPath)
    {
        Id = Guid.NewGuid();
        Price = price;
        Description = description;
        CategoryName = categoryName;
        ImageUrl = imageUrl;
        ImageLocalPath = imageLocalPath;
    }
    public string Name { get; protected set; }
    [Range(1, 10000)]
    public double Price { get; protected set; }
    public string Description { get; protected set; }
    public string CategoryName { get; protected set; }
    public string? ImageUrl { get; protected set; }
    public string? ImageLocalPath { get; protected set; }

    public void SetImageUrl(string imageUrl)
    { 
        ImageUrl = imageUrl; 
    }
    public void SetImageLocalPath(string imageLocalPath)
    {
        ImageLocalPath = imageLocalPath;
    }
}
