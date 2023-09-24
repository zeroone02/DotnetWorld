using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;

namespace DotnetWorld.WebService.Application;
public class CouponService : ICouponService
{
    public readonly IBaseService _baseService;

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Url = SD.CouponAPIBase + "/api/coupon",
            Data = couponDto
        });
    }

    public async Task<ResponseDto?> DeleteCouponAsync(Guid couponId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.DELETE,
            Url = SD.CouponAPIBase + "/api/coupon/deleteCoupon/" + couponId
        });
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType =  SD.ApiType.GET,
            Url = SD.CouponAPIBase + "/api/coupon/list"
        });
    }

    public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
        });
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(Guid couponId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.CouponAPIBase + "/api/coupon/GetById/" + couponId
        });
    }

    public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.PUT,
            Url = SD.CouponAPIBase + "/api/coupon",
            Data = couponDto
        });
    }
}
