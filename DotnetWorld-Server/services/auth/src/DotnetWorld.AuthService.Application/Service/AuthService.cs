using DotnetWorld.AuthService.Application.Contracts;
using DotnetWorld.AuthService.Domain;
using eShop.AuthService.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DotnetWorld.AuthService.Application;
public class AuthService : IAuthService
{
    public readonly AuthServiceDbContext _db;
    public readonly UserManager<ApplicationUser> _userManager;
    public readonly RoleManager<IdentityRole> _roleManager;
    public AuthService(AuthServiceDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                return "";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }

        }
        catch (Exception ex)
        {

        }
        return "Error Encountered";
    }
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
       var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
        bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
        if (user == null || isValid == false)
        {
            return new LoginResponseDto() { User = null, Token = "" };
        }
        var roles = await _userManager.GetRolesAsync(user);
        //var token = _jwtTokenGenerator.GenerateToken(user, roles);
        UserDto userDto = new()
        {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };
        LoginResponseDto loginResponseDto = new()
        {
            User = userDto,
            //Token = token
        };
        return loginResponseDto;
    }
}
