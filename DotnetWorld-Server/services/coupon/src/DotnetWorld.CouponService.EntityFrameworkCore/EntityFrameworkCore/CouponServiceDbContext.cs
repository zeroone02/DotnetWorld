using DotnetWorld.CouponService.Domain;
using DotnetWorld.DDD;
using Microsoft.EntityFrameworkCore;

namespace DotnetWorld.CouponService.EntityFrameworkCore;
public class CouponServiceDbContext : DbContext, IEfCoreDbContext
{
   public CouponServiceDbContext(DbContextOptions<CouponServiceDbContext> options )
        : base(options)
    {

    }
    public DbSet<Coupon> Coupons { get; set; }
}
