using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
    {
        ResponseDto responseDto = await _authService.LoginAsync(loginRequestDto);
        if (responseDto != null && responseDto.IsSuccess)
        {
            LoginResponseDto loginResponseDto =
                JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));
            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["error"] = responseDto.Message;
            return View(loginRequestDto);
        }
    }
    [HttpGet]
    public IActionResult Register()
    {
        var roleList = new List<SelectListItem>()
        {
            new SelectListItem{Text = SD.RoleAdmin,Value = SD.RoleAdmin},
            new SelectListItem{Text = SD.RoleCustomer,Value = SD.RoleCustomer}
        };
        ViewBag.RoleList = roleList;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
    {
        ResponseDto result = await _authService.RegisterAsync(registrationRequestDto);
        ResponseDto assignRole;
        if (result != null && result.IsSuccess)
        {
            if (string.IsNullOrEmpty(registrationRequestDto.Role))
            {
                registrationRequestDto.Role = SD.RoleCustomer;
            }
            assignRole = await _authService.AssignRoleAsync(registrationRequestDto);
            if (assignRole != null && result.IsSuccess)
            {
                TempData["success"] = "Registration Successful";
                return RedirectToAction(nameof(Login));   
            }
        }
        return View();
    }
    public IActionResult Logout()
    {
        return View();
    }
}
