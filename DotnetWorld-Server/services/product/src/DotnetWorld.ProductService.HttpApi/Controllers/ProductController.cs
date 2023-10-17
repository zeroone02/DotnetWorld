using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DotnetWorld.ProductService.Application.Contracts;
using DotnetWorld.DDD;

namespace DotnetWorld.ProductService.HttpApi.Host.Controllers;

[Route("api/product")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ResponseDto _response;
    public ProductController(IProductService productService)
    {
        _productService = productService;
        _response = new ResponseDto();
    }
    [HttpGet]
    public async Task<ResponseDto> GetList()
    {
        try
        {
            var result  = await _productService.GetListAsync();
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
            var result = await _productService.GetAsync(id);
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
    public async Task<ResponseDto> Create([FromForm] CreateProductDto createProductDto)
    {
        try
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                $"{HttpContext.Request.PathBase.Value}";
         
            var result = await _productService.CreateProductAsync(createProductDto, baseUrl);
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
    [Route("deleteProduct/{id}")]
    public async Task<ResponseDto> Delete(Guid id)
    {
        try
        {
             await _productService.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;

    }
    [HttpPut]
    public async Task<ResponseDto> Update([FromForm] UpdateProductDto updateProductDto)
    {
        try
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                $"{HttpContext.Request.PathBase.Value}";

            var result = await _productService.UpdateProductAsync(updateProductDto, baseUrl);
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
