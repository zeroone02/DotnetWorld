using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.WebService.Application.Contracts;
public class UpdateCouponDto : EntityDto<Guid> 
{
    public string CouponCode { get; set; }
    public double DiscountAmount { get; set; }
    public int MinAmount { get; set; }
}
