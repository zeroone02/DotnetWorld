using DotnetWorld.DDD.Entities;

namespace DotnetWorld.CouponService.Domain;
public class Coupon : AggregateRoot<Guid>
{
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}
