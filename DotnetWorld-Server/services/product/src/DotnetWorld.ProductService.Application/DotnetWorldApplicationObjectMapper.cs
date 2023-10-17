using AutoMapper;
using DotnetWorld.ProductService.Application.Contracts;
using DotnetWorld.ProductService.Domain;
using DotnetWorld.DDD;

namespace DotnetWorld.ProductService.Application;
public class DotnetWorldApplicationObjectMapper : Profile
{
    public DotnetWorldApplicationObjectMapper()
    {
        MapProducts();
    }

    private void MapProducts()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, CreateProductDto>().ReverseMap()
            .Ignore(x => x.Id);

        CreateMap<Product, UpdateProductDto>().ReverseMap()
            .Ignore(x => x.Id);
    }
   
}
