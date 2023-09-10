using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.CouponService.Application.Contracts;
public class CouponDto : EntityDto<Guid>
{
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}
