﻿using DotnetWorld.CouponService.Application.Contracts;
using DotnetWorld.DDD;
using DotnetWorld.DDD.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
    [HttpGet]
    public async Task<ResponseDto> GetList(PagedRequestDto pagedRequestDto)
    {
        try
        {
            var result = await _couponService.GetListAsync(pagedRequestDto);
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
    public async Task<ResponseDto> Update(Guid id,[FromBody] UpdateCouponDto couponDto)
    {
        try
        {
            var result = await _couponService.UpdateAsync(id,couponDto);
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

