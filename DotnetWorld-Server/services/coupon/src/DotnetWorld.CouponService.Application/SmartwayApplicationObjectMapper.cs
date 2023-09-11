using AutoMapper;
using DotnetWorld.CouponService.Application.Contracts;
using DotnetWorld.CouponService.Domain;
using DotnetWorld.DDD;

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
        CreateMap<Coupon, CreateCouponDto>().ReverseMap()
            .Ignore(x => x.Id);

        CreateMap<Coupon, UpdateCouponDto>().ReverseMap()
            .Ignore(x => x.Id);
    }
   
}
