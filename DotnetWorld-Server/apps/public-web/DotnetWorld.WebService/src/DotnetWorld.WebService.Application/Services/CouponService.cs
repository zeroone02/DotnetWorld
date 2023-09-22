using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;

namespace DotnetWorld.WebService.Application;
public class CouponService : ICouponService
{
    public readonly IBaseService _baseService;

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public Task<ResponseDto> CreateCouponAsync(CouponDto couponDto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> DeleteCouponAsync(Guid couponId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> GetAllCouponsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> GetCouponByCodeAsync(string couponCode)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> GetCouponByIdAsync(Guid couponId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto)
    {
        throw new NotImplementedException();
    }
}
