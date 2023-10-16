using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.ShoppingCartService.Domain;
public class CartHeaderDto : EntityDto<Guid>
{
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double CartTotal { get; set; }
}
