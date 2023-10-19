namespace DotnetWorld.WebService.Application.Contracts;
public class CartDto
{
    public UserCartDto UserCart { get; set; }
    public IEnumerable<CartDetailDto>? CartDetails { get; set; }
}
