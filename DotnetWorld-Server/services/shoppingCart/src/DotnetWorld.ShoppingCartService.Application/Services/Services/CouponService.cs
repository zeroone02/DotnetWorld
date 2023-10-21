using DotnetWorld.DDD;
using DotnetWorld.ShoppingCartService.Application.Contracts;
using DotnetWorld.ShoppingCartService.Domain;
using Newtonsoft.Json;

namespace DotnetWorld.ShoppingCartService.Application;
public class CouponService : ICouponService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CouponService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<CouponDto> GetCoupon(string couponCode)
    {
        var client = _httpClientFactory.CreateClient("Coupon");
        var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");
        var apiContent = await response.Content.ReadAsStringAsync();
        var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
        if (resp != null && resp.IsSuccess)
        {
            return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
        }
        return new CouponDto();
    }
}
