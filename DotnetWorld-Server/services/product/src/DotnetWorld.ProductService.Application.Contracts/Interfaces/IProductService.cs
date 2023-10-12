
using DotnetWorld.ProductService.Domain;

namespace DotnetWorld.ProductService.Application.Contracts;
public interface IProductService
{
    public Task<IEnumerable<ProductDto>> GetListAsync();
    public Task<ProductDto> GetAsync(Guid id);
    public Task<Product> AddAsync(ProductDto ProductDto);
    public Task<Product> DeleteAsync(Guid id);
    public Task<ProductDto> UpdateAsync(ProductDto ProductDto);
}
