﻿using DotnetWorld.CouponService.Domain;
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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(new Coupon("10OFF", 10, 10));

        //modelBuilder.Entity<Coupon>().HasData(new Coupon
        //{
        //    Id = Guid.NewGuid(),
        //    CouponCode = "20OFF",
        //    DiscountAmount = 20,
        //    MinAmount = 40,
        //});

        //modelBuilder.Entity<Coupon>().HasData(new Coupon
        //{
        //    Id = Guid.NewGuid(),
        //    CouponCode = "30OFF",
        //    DiscountAmount = 30,
        //    MinAmount = 600,
        //});
    }
}
