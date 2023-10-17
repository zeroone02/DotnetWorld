using DotnetWorld.ShoppingCartService.Domain;

namespace DotnetWorld.ShoppingCartService.Application.Contracts;
public interface ICartService
{
    public Task<CartDto> GetCart(string userId);
    public Task<CartDto> CartUpsert(CartDto cartDto);
    public Task<bool> RemoveCart(Guid cartDetailId);
    public Task<CartDto> ApplyCoupon(CartDto cartDto);
    public Task<bool> RemoveCoupon(CartDto cartDto);
   
}
