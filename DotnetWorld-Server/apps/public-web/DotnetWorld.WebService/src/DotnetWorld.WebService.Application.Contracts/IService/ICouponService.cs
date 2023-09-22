using DotnetWorld.DDD;

namespace DotnetWorld.WebService.Application.Contracts;
public interface ICouponService
{
    Task<ResponseDto> GetCouponByCodeAsync(string couponCode);
    Task<ResponseDto> GetCouponByIdAsync(Guid couponId);
    Task<ResponseDto> GetAllCouponsAsync();
    Task<ResponseDto> CreateCouponAsync (CouponDto couponDto);
    Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto);
    Task<ResponseDto> DeleteCouponAsync(Guid couponId);
}
