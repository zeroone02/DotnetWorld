using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;

namespace DotnetWorld.WebService.Application;
public class CartService : ICartService
{
    public readonly IHttpClientService _httpClientService;
    public CartService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }
    public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
    {
        return await _httpClientService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.CartAPIBase + "/api/cart/ApplyCoupon"
        });
    }

    public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
    {
        return await _httpClientService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.CartAPIBase + "/api/cart/GetCart/" + userId
        });
    }

    public async Task<ResponseDto?> RemoveFromCartAsync(Guid cartDetailsId)
    {
        return await _httpClientService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDetailsId,
            Url = SD.CartAPIBase + "/api/cart/RemoveCart"
        });
    }

    public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
    {
        return await _httpClientService.SendAsync(new RequestDto()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.CartAPIBase + "/api/cart/CartUpsert"
        });
    }
}
