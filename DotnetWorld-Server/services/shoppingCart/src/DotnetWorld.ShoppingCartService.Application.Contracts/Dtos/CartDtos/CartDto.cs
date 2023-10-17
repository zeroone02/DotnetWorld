namespace DotnetWorld.ShoppingCartService.Domain;
public class CartDto
{
    public UserCartDto CartHeader { get; set; }
    public IEnumerable<CartDetailDto>? CartDetails { get; set; }
}
