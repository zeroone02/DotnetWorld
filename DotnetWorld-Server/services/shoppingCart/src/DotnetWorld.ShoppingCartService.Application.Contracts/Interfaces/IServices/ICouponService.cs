using DotnetWorld.ShoppingCartService.Domain;
namespace DotnetWorld.ShoppingCartService.Application.Contracts;
public interface ICouponService
{
    Task<CouponDto> GetCoupon(string couponCode);
}
