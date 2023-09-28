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

    public Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto> Register(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }
}
