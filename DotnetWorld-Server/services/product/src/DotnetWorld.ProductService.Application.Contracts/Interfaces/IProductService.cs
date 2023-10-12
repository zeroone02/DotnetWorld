using DotnetWorld.DDD.Application.Contracts;

namespace DotnetWorld.ProductService.Application.Contracts;
public interface IProductService :
     ICrudAppService<ProductDto, Guid, CreateProductDto, UpdateProductDto>
{
    public Task<ProductDto> CreateProductAsync(CreateProductDto input, string inputBaseUrl);
    public Task<ProductDto> UpdateProductAsync(UpdateProductDto input, string inputBaseUrl);

}
