using DotnetWorld.DDD;
using DotnetWorld.ShoppingCartService.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotnetWorld.ShoppingCartService.EntityFrameworkCore;
public class ShoppingCartServiceDbContext : DbContext, IEfCoreDbContext
{
    public ShoppingCartServiceDbContext(DbContextOptions<ShoppingCartServiceDbContext> options)
         : base(options)
    {

    }
    public DbSet<UserCart> UserCarts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }

}
