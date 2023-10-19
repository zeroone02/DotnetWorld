using DotnetWorld.DDD.Application.Contracts;
namespace DotnetWorld.WebService.Application.Contracts;
public class CartDetailDto : EntityDto<Guid>
{
    public Guid UserCartId { get; set; }
    public UserCartDto? UserCart { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
}
