using DotnetWorld.WebService.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DotnetWorld.WebService.Controllers;
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        LoginRequestDto loginRequestDto = new();
        return View(loginRequestDto);
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    public IActionResult Logout()
    {
        return View();
    }
}
