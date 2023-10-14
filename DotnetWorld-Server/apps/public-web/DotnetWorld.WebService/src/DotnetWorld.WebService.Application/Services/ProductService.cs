using DotnetWorld.DDD;
using DotnetWorld.Web.Application.Contracts;
using DotnetWorld.Web.Domain;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;

namespace DotnetWorld.Web.Application;
public class ProductService : IProductService
{
    private readonly IHttpClientService _httpClientService;
    public ProductService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }
	public async Task<ResponseDto?> CreateProductsAsync(ProductDto productDto)
	{
		return await _httpClientService.SendAsync(new RequestDto()
		{
			ApiType = SD.ApiType.POST,
			Data = productDto,
			Url = SD.ProductAPIBase + "/api/product",
			ContentType = SD.ContentType.MultipartFormData
		});
	}

	public async Task<ResponseDto?> DeleteProductAsync(Guid id)
	{
		return await _httpClientService.SendAsync(new RequestDto()
		{
			ApiType = SD.ApiType.DELETE,
			Url = SD.ProductAPIBase + "/api/product/deleteProduct/" + id
		});
	}

	public async Task<ResponseDto?> GetAllProductAsync()
    {
		return await _httpClientService.SendAsync(new RequestDto()
		{
			ApiType = SD.ApiType.GET,
			Url = SD.ProductAPIBase + "/api/product"
		});
	}

    public async Task<ResponseDto?> GetProductByIdAsync(Guid id)
    {
        return await _httpClientService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.ProductAPIBase + "/api/product/GetById/" + id
        });
    }

	public async Task<ResponseDto?> UpdateProductAsync(ProductDto productDto)
	{
		return await _httpClientService.SendAsync(new RequestDto()
		{
			ApiType = SD.ApiType.PUT,
			Data = productDto,
			Url = SD.ProductAPIBase + "/api/product",
			ContentType = SD.ContentType.MultipartFormData
		});
	}
}
