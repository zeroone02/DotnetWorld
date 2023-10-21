using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.WebService.Application.Contracts;
public class UserCartDto : EntityDto<Guid>
{
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double CartTotal { get; set; }
}
