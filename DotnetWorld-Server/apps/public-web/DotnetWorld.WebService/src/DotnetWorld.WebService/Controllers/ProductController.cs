using DotnetWorld.DDD;
using DotnetWorld.Web.Application.Contracts;
using DotnetWorld.WebService.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotnetWorld.Web.Controllers;
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto>? list = new();
        ResponseDto? response = await _productService.GetAllProductAsync();
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return View(list);
    }
    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductDto model)
    {
        model.Id = Guid.NewGuid();
		if (ModelState.IsValid)
		{
			ResponseDto? response = await _productService.CreateProductsAsync(model);

			if (response != null && response.IsSuccess)
			{
				TempData["success"] = "Product created successfully";
				return RedirectToAction(nameof(ProductIndex));
			}
			else
			{
				TempData["error"] = response?.Message;
			}
		}
		return View(model);
	}
    public async Task<IActionResult> ProductDelete(Guid id)
    {
        ResponseDto? response = await _productService.GetProductByIdAsync(id);

        if (response != null && response.IsSuccess)
        {
            ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductDto productDto)
    {
        ResponseDto? response = await _productService.DeleteProductAsync(productDto.Id);

        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "product deleted successfully";
            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(productDto);
    }
    public async Task<IActionResult> ProductEdit(Guid id)
    {
        ResponseDto? response = await _productService.GetProductByIdAsync(id);

        if (response != null && response.IsSuccess)
        {
            ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ProductEdit(ProductDto productDto)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _productService.UpdateProductAsync(productDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
        }
        return View(productDto);
    }
}