
using DotnetWorld.DDD;
using DotnetWorld.ProductService.Application.Contracts;
using DotnetWorld.ProductService.Domain;

namespace DotnetWorld.ProductService.Application;
public class ProductService : IProductService
{
    public Task<Product> AddAsync(ProductDto ProductDto)
    {
        throw new NotImplementedException();
    }

    public Task<Product> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductDto>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> UpdateAsync(ProductDto ProductDto)
    {
        throw new NotImplementedException();
    }
}
