using AutoMapper;
using DotnetWorld.DDD;

namespace DotnetWorld.ShoppingCartService.Application;
public class DotnetWorldApplicationObjectMapper : Profile
{
    public DotnetWorldApplicationObjectMapper()
    {
        MapCartItems();
    }

    private void MapCartItems()
    {
        //CreateMap<Coupon, CouponDto>().ReverseMap();
        //CreateMap<Coupon, CreateCouponDto>().ReverseMap()
        //    .Ignore(x => x.Id);

        //CreateMap<Coupon, UpdateCouponDto>().ReverseMap()
        //    .Ignore(x => x.Id);
    }
   
}
