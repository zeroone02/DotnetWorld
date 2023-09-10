using AutoMapper;
using DotnetWorld.CouponService.Application.Contracts;
using DotnetWorld.CouponService.Domain;

namespace DotnetWorld.Application;
public class SmartwayApplicationObjectMapper : Profile
{
    public SmartwayApplicationObjectMapper()
    {
        MapCoupons();
    }

    private void MapCoupons()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
   
}
