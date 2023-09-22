namespace DotnetWorld.WebService.Application.Contracts;
public class CreateCouponDto
{
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}
