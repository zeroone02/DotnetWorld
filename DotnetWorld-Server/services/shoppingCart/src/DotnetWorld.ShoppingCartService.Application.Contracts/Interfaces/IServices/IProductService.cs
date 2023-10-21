using DotnetWorld.ShoppingCartService.Domain;

namespace DotnetWorld.ShoppingCartService.Application.Contracts;
public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}
