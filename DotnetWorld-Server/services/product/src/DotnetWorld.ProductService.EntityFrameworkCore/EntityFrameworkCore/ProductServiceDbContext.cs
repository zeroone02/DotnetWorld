using DotnetWorld.DDD;
using Microsoft.EntityFrameworkCore;
using DotnetWorld.ProductService.Domain;
namespace DotnetWorld.ProductService.EntityFrameworkCore;
public class ProductServiceDbContext : DbContext, IEfCoreDbContext
{
   public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options )
        : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }
   
}
