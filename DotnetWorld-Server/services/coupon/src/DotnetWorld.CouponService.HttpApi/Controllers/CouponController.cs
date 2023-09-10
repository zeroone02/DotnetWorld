using DotnetWorld.CouponService.Application.Contracts;
using DotnetWorld.DDD.Application.Contracts;
using DotnetWorld.DDD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShop.CouponService.HttpApi.Host.Controllers;

[Route("api/coupon")]
[ApiController]
public class CouponController : ControllerBase
{
    private ICouponService _couponService;
    private UnitOfWork _unitOfWork;
    private ResponseDto _response;
    public CouponController(ICouponService couponService, UnitOfWork unitOfWork)
    {
        _couponService = couponService;
        _unitOfWork = unitOfWork;
        _response = new ResponseDto();
    }
    [HttpGet]
    public async Task<ResponseDto> GetList(int skip, int take)
    {
        try
        {
            var result  = await _couponService.GetListAsync(skip, take);
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

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Create([FromBody] CouponDto couponDto)
    {
        try
        {
            var result = await _couponService.AddAsync(couponDto);
            await _unitOfWork.SaveChangesAsync();
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
    [Route("deleteCoupon/{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Delete(Guid id)
    {
        try
        {
            var result = await _couponService.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            _response.Result = result;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Update([FromBody] CouponDto couponDto)
    {
        try
        {
            var result = await _couponService.UpdateAsync(couponDto);
            await _unitOfWork.SaveChangesAsync();
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
