using DotnetWorld.CouponService.Application.Contracts;
using DotnetWorld.DDD;
using DotnetWorld.DDD.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DotnetWorld.CouponService.HttpApi;

[Route("api/coupon")]
[ApiController]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;
    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }
    [HttpGet("list")]
    public async Task<ResponseDto<PagedResultDto<CouponDto>>> GetList([FromQuery] PagedRequestDto pagedRequestDto)
    {
        ResponseDto<PagedResultDto<CouponDto>> response = new();
        try
        {
            var result = await _couponService.GetListAsync(pagedRequestDto);
           
            response.Result = result;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }
    [HttpGet]
    [Route("GetById/{id}")]
    public async Task<ResponseDto<CouponDto>> GetById(Guid id)
    {
        ResponseDto<CouponDto> response = new();
        try
        {
            var result = await _couponService.GetAsync(id);
            response.Result = result;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }
    [HttpGet]
    [Route("GetByCode/{code}")]
    public async Task<ResponseDto<CouponDto>> GetByCode(string code)
    {
        ResponseDto<CouponDto> response = new();
        try
        {
            var result = await _couponService.GetByCodeAsync(code);
            response.Result = result;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }
    [HttpPost]
    public async Task<ResponseDto<CouponDto>> Create([FromBody] CreateCouponDto couponDto)
    {
        ResponseDto<CouponDto> response = new();
        try
        {
            var result = await _couponService.CreateAsync(couponDto);
            response.Result = result;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }
    [HttpDelete]
    public async Task<ResponseDto<CouponDto>> Delete(Guid id)
    {
        ResponseDto<CouponDto> response = new();
        try
        {
            await _couponService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }

    [HttpPut]
    public async Task<ResponseDto<CouponDto>> Update(Guid id, [FromBody] UpdateCouponDto couponDto)
    {
        ResponseDto<CouponDto> response = new();
        try
        {
            var result = await _couponService.UpdateAsync(id, couponDto);
            response.Result = result;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }
}

