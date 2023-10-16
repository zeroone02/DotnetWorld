using DotnetWorld.DDD;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetWorld.ShoppingCartService.Domain;
public class CartDetail : Entity<Guid>
{
    public Guid CartHeaderId { get; set; }
    [ForeignKey("CartHeaderId")]
    public CartHeader CartHeader { get; set; }
    public Guid ProductId { get; set; }
    //[NotMapped]
    //public ProductDto Product { get; set; }
    public int Count { get; set; }
}
