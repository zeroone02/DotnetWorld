using DotnetWorld.DDD;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetWorld.ShoppingCartService.Domain;
public class CartHeader : AggregateRoot<Guid>
{
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }

    [NotMapped]
    public double Discount { get; set; }
    [NotMapped]
    public double CartTotal { get; set; }
}
