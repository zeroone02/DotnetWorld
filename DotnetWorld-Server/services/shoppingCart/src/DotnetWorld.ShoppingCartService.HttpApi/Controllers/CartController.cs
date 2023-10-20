using DotnetWorld.DDD;
using DotnetWorld.ShoppingCartService.Application.Contracts;
using DotnetWorld.ShoppingCartService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotnetWorld.ShoppingCartService.HttpApi;
[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ResponseDto _response;
    private readonly ICartService _cartService;
    public CartController(ICartService cartService)
    {
        _response = new ResponseDto();
        _cartService = cartService;
    }
    [HttpGet("GetCart/{userId}")]
    public async Task<ResponseDto> GetCart(string userId)
    {
        try
        {
            var response = await _cartService.GetCart(userId);
            _response.Result = response;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpPost("ApplyCoupon")]
    public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
    {
        try
        {
            var response = await _cartService.ApplyCoupon(cartDto);
            _response.Result = response;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpPost("RemoveCoupon")]
    public async Task<object> RemoveCoupon([FromBody] CartDto cartDto)
    {
        try
        {
            var response = await _cartService.RemoveCoupon(cartDto);
            _response.Result = response;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpPost("CartUpsert")]
    public async Task<ResponseDto> CartUpsert(CartDto cartDto)
    {
        try
        {
            var response = await _cartService.CartUpsert(cartDto);
            _response.Result = response;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpPost("RemoveCart")]
    public async Task<ResponseDto> RemoveCart([FromBody] Guid cartDetailsId)
    {
        try
        {
            var response = await _cartService.RemoveCart(cartDetailsId);
            _response.Result = response;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
}
