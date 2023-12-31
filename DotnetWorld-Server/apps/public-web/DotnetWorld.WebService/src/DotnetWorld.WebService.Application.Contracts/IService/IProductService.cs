﻿using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;

namespace DotnetWorld.Web.Application.Contracts;
public interface IProductService
{
    Task<ResponseDto?> GetAllProductAsync();
    Task<ResponseDto?> GetProductByIdAsync(Guid id);
    Task<ResponseDto?> CreateProductsAsync(ProductDto productDto);
    Task<ResponseDto?> DeleteProductAsync(Guid id);
    Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
}
