using DotnetWorld.DDD;
using DotnetWorld.Web.Application.Contracts;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;
using DotnetWorld.WebService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DotnetWorld.WebService.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
	private readonly IProductService _productService;

	public HomeController(ILogger<HomeController> logger, IProductService productService)
	{
		_logger = logger;
		_productService = productService;
	}

	public async Task<IActionResult> Index()
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
	[Authorize]
	public async Task<IActionResult> ProductDetails(Guid id)
	{
		ProductDto? model = new();

		ResponseDto? response = await _productService.GetProductByIdAsync(id);

		if (response != null && response.IsSuccess)
		{
			model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
		}
		else
		{
			TempData["error"] = response?.Message;
		}

		return View(model);
	}
	//[Authorize]
	//[HttpPost]
	//[ActionName("ProductDetails")]
	//public async Task<IActionResult> ProductDetails(ProductDto productDto)
	//{
	//	CartDto cartDto = new CartDto()
	//	{
	//		CartHeader = new CartHeaderDto
	//		{
	//			UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject)?.FirstOrDefault()?.Value
	//		}
	//	};

	//	CartDetailsDto cartDetails = new CartDetailsDto()
	//	{
	//		Count = productDto.Count,
	//		ProductId = productDto.Id,
	//	};

	//	List<CartDetailsDto> cartDetailsDtos = new() { cartDetails };
	//	cartDto.CartDetails = cartDetailsDtos;

	//	ResponseDto? response = await _shoppingCartService.UpsertCartAsync(cartDto);

	//	if (response != null && response.IsSuccess)
	//	{
	//		TempData["success"] = "Item has been added to the Shopping Cart";
	//		return RedirectToAction(nameof(Index));
	//	}
	//	else
	//	{
	//		TempData["error"] = response?.Message;
	//	}

	//	return View(productDto);
	//}
	[Authorize(Roles = SD.RoleAdmin)]
	public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
