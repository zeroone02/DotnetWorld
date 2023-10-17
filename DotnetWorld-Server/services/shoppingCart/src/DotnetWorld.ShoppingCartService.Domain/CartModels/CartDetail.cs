using DotnetWorld.DDD;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetWorld.ShoppingCartService.Domain;
public class CartDetail : Entity<Guid>
{
    public Guid UserCartId { get; set; }
    [ForeignKey("UserCartId")]
    public UserCart UserCart { get; set; }
    public Guid ProductId { get; set; }
    [NotMapped]
    public ProductDto Product { get; set; }
    public int Count { get; set; }
}
