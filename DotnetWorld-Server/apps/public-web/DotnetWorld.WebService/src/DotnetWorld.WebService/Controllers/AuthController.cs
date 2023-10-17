using DotnetWorld.DDD;
using DotnetWorld.WebService.Application.Contracts;
using DotnetWorld.WebService.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotnetWorld.WebService.Controllers;
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ITokenProvider _tokenProvider;
    public AuthController(IAuthService authService, ITokenProvider tokenProvider)
    {
        _authService = authService;
        _tokenProvider = tokenProvider;
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
            await SignInUser(loginResponseDto);
            
            _tokenProvider.SetToken(loginResponseDto.Token);
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
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        return RedirectToAction("Index", "Home");
    }
    private async Task SignInUser(LoginResponseDto model)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(model.Token);
        //данные пользователя
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


        identity.AddClaim(new Claim(ClaimTypes.Name,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role,
            jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


        //Это единое утверждение, содержащее набор claim.
        var principal = new ClaimsPrincipal(identity);

        //создать cookie, содержащий информацию о пользователе,
        //HttpContext.SignInAsync сериализует principal
        //и поместит его в зашифрованный cookie,
        //который в свою очередь будет прикреплен к ответу веб - сервера и сохранен на стороне клиента:
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}
