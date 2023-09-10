namespace DotnetWorld.CouponService.Application.Contracts;
public class UpdateCouponDto
{
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}
