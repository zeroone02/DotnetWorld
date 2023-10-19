using AutoMapper;
using DotnetWorld.DDD;
using DotnetWorld.ShoppingCartService.Domain;

namespace DotnetWorld.ShoppingCartService.Application;
public class DotnetWorldApplicationObjectMapper : Profile
{
    public DotnetWorldApplicationObjectMapper()
    {
        MapCartItems();
    }

    private void MapCartItems()
    {
        CreateMap<UserCart, UserCartDto>().ReverseMap();
        CreateMap<CartDetail, CartDetailDto>().ReverseMap();
    }
   
}
