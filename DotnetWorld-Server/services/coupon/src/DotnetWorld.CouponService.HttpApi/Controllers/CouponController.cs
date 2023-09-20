using DotnetWorld.CouponService.Application.Contracts;
using DotnetWorld.DDD;
using Microsoft.AspNetCore.Mvc;

namespace DotnetWorld.CouponService.HttpApi;

[Route("api/coupon")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;
    private readonly ResponseDto _response;
    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
        _response = new ResponseDto();
    }
    [HttpGet("list")]
    public async Task<ResponseDto> GetList()
    {
        try
        {
            var result = await _couponService.GetListAsync();

            _response.Result = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpGet]
    [Route("GetById/{id}")]
    public async Task<ResponseDto> GetById(Guid id)
    {
        try
        {
            var result = await _couponService.GetAsync(id);
            _response.Result = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpGet]
    [Route("GetByCode/{code}")]
    public async Task<ResponseDto> GetByCode(string code)
    {
        try
        {
            var result = await _couponService.GetByCodeAsync(code);
            _response.Result = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpPost]
    public async Task<ResponseDto> Create([FromBody] CreateCouponDto couponDto)
    {
        try
        {
            var result = await _couponService.CreateAsync(couponDto);
            _response.Result = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpDelete]
    public async Task<ResponseDto> Delete(Guid id)
    {
        try
        {
            await _couponService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpPut]
    public async Task<ResponseDto> Update([FromBody] UpdateCouponDto couponDto)
    {
        try
        {
            var result = await _couponService.UpdateAsync(couponDto);
            _response.Result = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
}

