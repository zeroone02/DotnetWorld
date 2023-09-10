using DotnetWorld.DDD;

namespace DotnetWorld.CouponService.Domain;
public class Coupon : AggregateRoot<Guid>
{
    internal Coupon(Coupon coupon)
    {
        Id = coupon.Id;
        CouponCode = coupon.CouponCode;
        DiscountAmount = coupon.DiscountAmount;
        MinAmount = coupon.MinAmount;
    }
    internal Coupon(Guid id, string couponCode, double discountAmount, int minAmount)
    {
        Id = id;
        CouponCode = couponCode;
        DiscountAmount = discountAmount;
        MinAmount = minAmount;
    }
    public Coupon(string couponCode, double discountAmount, int minAmount)
    {
        Id = Guid.NewGuid();
        CouponCode = couponCode;
        DiscountAmount = discountAmount;
        MinAmount = minAmount;
    }

    public string CouponCode { get; protected set; }
    public double DiscountAmount { get; protected set; }
    public int MinAmount { get; protected set; }
}
