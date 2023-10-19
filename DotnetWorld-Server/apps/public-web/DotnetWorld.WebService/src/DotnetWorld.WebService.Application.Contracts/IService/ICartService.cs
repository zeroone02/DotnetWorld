using DotnetWorld.DDD;

namespace DotnetWorld.WebService.Application.Contracts;
public interface ICartService
{
    Task<ResponseDto?> GetCartByUserIdAsync(string userId);
    Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
    Task<ResponseDto?> RemoveFromCartAsync(Guid cartDetailsId);
    Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
}
